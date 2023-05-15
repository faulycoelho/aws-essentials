﻿using Amazon.SQS;
using Amazon.SQS.Model;

var queueName = args.Length == 1 ? args[0] : "users";

var cts = new CancellationTokenSource();
var sqsClient = new AmazonSQSClient();

var queueUrlResponse = await sqsClient.GetQueueUrlAsync(queueName);

var receiveMessageRequest = new ReceiveMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    AttributeNames = new List<string> { "All" }, //force to return attributes, eg.: 
    MessageAttributeNames = new List<string> { "All" } //force to return message attributes

};

while (!cts.IsCancellationRequested)
{
    var response = await sqsClient.ReceiveMessageAsync(receiveMessageRequest, cts.Token);
    foreach (var message in response.Messages)
    {
        Console.WriteLine($"Nessage Id: {message.MessageId}");
        Console.WriteLine($"Nessage Body: {message.Body}");
        //delete from the queue
        await sqsClient.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle);

    }
    await Task.Delay(1000);
}