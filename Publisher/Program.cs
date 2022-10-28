using Amazon.SQS;
using Publisher;
using Publisher.Messages;

var sqsClient =  new AmazonSQSClient(Amazon.RegionEndpoint.SAEast1);

var publisher = new SqsPublisher(sqsClient);

await publisher.PublishAsync("customers", new CustomerCreated{
  Id = 1,
  FullName = "Ocione"
});
