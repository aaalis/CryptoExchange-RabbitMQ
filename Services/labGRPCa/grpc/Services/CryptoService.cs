using Google.Protobuf;
using Grpc.Core;
using labGRPCa;
using labGRPCa.Model;
using labGRPCa.Repositories;
using System;

namespace labGRPCa.Services
{
    public class CryptoService : CryptoGrpcService.CryptoGrpcServiceBase
    {
        private readonly IRateRepository _rateRepository;

        public CryptoService(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        public override async Task<ResponseMessage> AddCurrencyRate(SingleCurrency request, ServerCallContext context)
        {
            var newRate = new CurrencyRate
            {
                DateOfChange = DateTimeFromStr(request.Date),
                Price = (decimal)request.Price,
                Currency = EnumFromStr<OrdersCurrency>(request.CurrencyName),
                BackRefAction = EnumFromStr<ActionType>(request.Action)
            };
            try
            {
                await _rateRepository.CreateRate(newRate);
            }
            catch (Exception e)
            {
                return new ResponseMessage { Content = "Exception has raised" };
            }

            return new ResponseMessage { Content = "Everything ok"};
        }
        public override async Task<AllChangesReply> GetAllChanges(AllCurrencyRequest request, ServerCallContext context)
        {
            var filter = new RateFilter
            {
                DateFrom = DateTimeFromStr(request.DateFrom),
                DateTo = DateTimeFromStr(request.DateTo),
                PriceFrom = (decimal)request.PriceFrom,
                PriceTo = (decimal)request.PriceTo,
                Actions = request.Actions.Select(a => EnumFromStr<ActionType>(a)).ToList(),
                Limit = request.Limit,
                Offset = request.Offset
            };


            var rates = await _rateRepository.GerRates(filter);
            
            int total = rates.Count();
            var Meta = new Meta
            {
                Total = total,
                Offset = request.Offset,
                Limit = request.Limit == default ? total : request.Limit
            };
            var reply = new AllChangesReply
            {
                Meta = Meta
            };
            reply.Data.AddRange(rates.Select(r => AdaptToChangesReply(r)));
            return reply;
        }
        public async override Task<ResponseMessage> AddCurrencyChangesStream(IAsyncStreamReader<SingleCurrency> requestStream, ServerCallContext context)
        {
            await foreach (SingleCurrency request in requestStream.ReadAllAsync())
            {
                await AddCurrencyRate(request, context);
            }
            return new ResponseMessage { Content = "Everything ok" };
        }
        public override async Task GetCurrencyChangesStream(ChangesRequest request, IServerStreamWriter<ChangesReply> responseStream, ServerCallContext context)
        {
            var rates = await _rateRepository.GerRates(new RateFilter
            {
                DateFrom = DateTimeFromStr(request.DateFrom),
                DateTo = DateTimeFromStr(request.DateTo),
                Currency = EnumFromStr<OrdersCurrency>(request.CurrencyName)
            });

            foreach (var rate in rates)
            {
                var reply = AdaptToChangesReply(rate);
                await responseStream.WriteAsync(reply);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        private ChangesReply AdaptToChangesReply(CurrencyRate rate)
        {
            var single = new SingleCurrency
            {
                CurrencyName = rate.Currency.ToString(),
                Action = rate.BackRefAction.ToString(),
                Date = rate.DateOfChange.ToString(),
                Price = (double)rate.Price
            };
            return new ChangesReply
            {
                ChangeId = rate.Id,
                Currency = single
            };
        }
        private DateTime DateTimeFromStr(string dateTimeStr)
        {
            if (string.IsNullOrEmpty(dateTimeStr))
                return default;
           return DateTime.Parse(dateTimeStr).ToUniversalTime();
        }
        private T EnumFromStr<T>(string enumStr)
            where T : Enum => (T)Enum.Parse(typeof(T), enumStr);

    }
}