# Unity-Dialogflow

## How to use this project
1. Clone the repository onto your local machine, and unzip the folder if necessary.
2. Follow the instructions given [here](https://cloud.google.com/dialogflow/es/docs/quick/setup) to set up  on Dialogflow. 
> If you followed the instructions correctly, you should have a JSON file downloaded onto your computer. 
Also, take note of your project ID, you will need this later.
### DO NOT share this file with anyone you do not trust.
3. Rename this JSON file to `application.json` and place it into the project directory.
4. Open up the Dialogflow console [https://dialogflow.cloud.google.com/].
5. In here you will set up your dialog that the bot will follow. Follow [this](https://cloud.google.com/dialogflow/es/docs/quick/build-agent) documentation for initial setup.
6. Once your agent is set up, here are some things you can do to make conversation:
- Click on `Create Intent` at the top right to create a new conversation thread.
> The box labeled `Add user expression` is the input, and the box labeled `Text Response` is what the bot will respond with
- The intent name does not matter much, but you are still free to use it! It is a part of the returned payload in Unity.
- You can test the bot in the top right box
- You can add follow-up intents 
7. In the `runScript.bat` file, find the parts labeled `[Put Project ID here]` and replace it with your agent's project ID.
> If you lost your project ID, you can find it [here](https://console.developers.google.com/) under the dropdown menu in the top left.
8. Once you are satisfied with the conversation in Dialogflow, you can open up the project in Unity.
9. Once the project is open in Unity, double click the `runScript.bat` file to run the API. 
> Note: if you run this script on a dedicated server rather than locally, you will need to change the line of code that has the web address. In this case, change it to the address the script is running on.
10. Everything should be all set up! Enter text in the text box and hit enter to see the response! 
> Developer's note: the response and intent are stored in the public variables `reply` and `intent`, respectively, when the code receives a reply. 

---
Feel free to contact me should any questions or confusion arise!
