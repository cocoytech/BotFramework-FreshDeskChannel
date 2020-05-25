# Custom FreshDesk channel for the Bot Framework
Initial quick and dirty draft. Code not optimized yet, can still contain bugs. Few todo's are marked still in code.

## Goal
- Provide a community-driven custom channel for the Microsoft Bot Framework to allow reading from, and sending responses to, support tickets in the [FreshDesk customer service application](https://freshdesk.com/). 

## Solution
- The FreshDesk custom channel for Bot Framework is implemented as an Azure function. When called, it checks for any new tickets or new ticket responses, and passes the ticket subject & description along to the Microsoft Bot Framework. 
- The custom FreshDesk channel can be invoked in two ways: 
  - Via a Timer-based trigger, which will poll the FreshDesk API every x time to fetch new tickets/responses. This method works for most FreshDesk plans that supports API calls. 
  - Via a HTTP trigger, which can be configured in specific FreshDesk plans as a webhook that is called when a ticket is updated. 
- The FreshDesk channel will use 2 containers on the same CosmosDB instance than the one used by Bot Framework:
  - *Freshdesk-botstate-container:* Link FreshDesk tickets to conversations happening in the Bot Framework
  - *LastRun*: Keep track of last execution (only processes differentials to save API limits)

## How to set things up
- Install your Bot logic (using BotFramework) on Azure. This should give you following components in Azure: App Service, CosmosDB, StorageAccount, Cognitive services (Luis). 
- Add the [Direct Line channel](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-channel-connect-directline?view=azure-bot-service-4.0) to your Bot in Azure. Copy the secret to **DirectLineSecret** and the name of the bot to **BotId** in the settings.json. 
- Add the CosmosDB **CosmosDBEndpointUri**, **CosmosDBPrimaryKey**, **CosmosDBDatabaseId**, **CosmosDBContainerId** connection information to the Settings.json.  
- Add the FreshDesk **FreshDeskClientUrl** and **FreshDeskAPIKey** to the Settings.json.
- Only newly updated tickets after the initial run will be processed, existing tickets will not be handled by Bot Framework. 

(Hint: The channel can be easily tested by using the EchoBot sample in Bot Framework Composer)

## Upcoming features
- Allow for delayed bot responses (for example when human confirmation is required before the bot sends a response back)
- Allow conversation termination when either a human agent is assigned, or the ticket status is marked as resolved
- Allow adding a private note for human engineer, instead of immediate responses to customer (via bot channeldata)
- Modifying ticket state after bot reply
- Provide additional insights in what the bot is doing
- Hand off ticket to human
- Trim signatures from the tickets that have as source email
