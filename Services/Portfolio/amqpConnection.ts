import portfolioService from './services/portfolio.service';

var amqp = require(`amqplib/callback_api`);

class AmqpConnection {
    connect() {
        amqp.connect('amqp://localhost', function(error0: any, connection: any) {
            if (error0) {
                throw error0;
            };

            connection.createChannel(function(error1: any, channel: any) {
                if (error1) {
                    throw error1;
                };

                channel.assertQueue('createPortfolio', {durable: false}, function(err: any, _ok: any) {
                    if(err) return;
                    
                    channel.consume('createPortfolio', function(msg:any) {
                        console.log(" [x] Received UserId:%s", msg.content.toString());
                        portfolioService.createPortfolio(msg.content);
                    });
                }); 

                channel.assertQueue('deletePortfolio', {durable: false}, function(err: any, _ok: any) {
                    if(err) return;
                    
                    channel.consume('deletePortfolio', function(msg:any) {
                        console.log(" [x] Received UserId:%s", msg.content.toString());
                        portfolioService.deletePortfolioByUserId(msg.content);
                    });
                })

                console.log("Waiting for messages.");

                // channel.consume(queue, function(msg:any) {
                //     console.log(" [x] Received UserId:%s", msg.content.toString());
                //     portfolioService.createPortfolio(msg.content);
                // });
            });
        });
    }
}

export default new AmqpConnection();