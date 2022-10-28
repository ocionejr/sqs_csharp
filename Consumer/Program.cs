using Amazon;
using Amazon.SQS;
using Consumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<SqsConsumerService>();
builder.Services.AddSingleton<IAmazonSQS>(_ => new AmazonSQSClient(RegionEndpoint.SAEast1));

var app = builder.Build();

app.Run();
