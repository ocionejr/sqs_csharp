using System.Net;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace Consumer;

public class SqsConsumerService : BackgroundService
{

  private readonly IAmazonSQS _sqs;
  private const string QueueName = "customers";
  private readonly List<string> _messageAttributeNames = new(){"All"};
  
  public SqsConsumerService(IAmazonSQS sqs)
  {
    _sqs = sqs;
  }

  protected override async Task ExecuteAsync(CancellationToken ct)
  {
    var queueUrl = await _sqs.GetQueueUrlAsync(QueueName, ct);
    var receiveRequest = new ReceiveMessageRequest{
      QueueUrl = queueUrl.QueueUrl,
      MessageAttributeNames = _messageAttributeNames,
      AttributeNames = _messageAttributeNames
    };

    while (!ct.IsCancellationRequested){
      var messageResponse = await _sqs.ReceiveMessageAsync(receiveRequest, ct);
      if (messageResponse.HttpStatusCode != HttpStatusCode.OK){
        // Fazer alguma coisa
        continue;
      }

      foreach (var message in messageResponse.Messages){
        Console.WriteLine(message.Body);
        await _sqs.DeleteMessageAsync(queueUrl.QueueUrl, message.ReceiptHandle, ct);
      }
    }
  }
}
