#!/usr/bin/env python

# Copyright 2017 Google LLC
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#      http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

import argparse

from google.cloud import dialogflow, storage

import os

from flask import Flask, request, jsonify

app = Flask(__name__)

os.environ["GOOGLE_APPLICATION_CREDENTIALS"] = "./authentication.json"


@app.route('/', methods = ['POST'])
def index():
    return RunDialogFlow(request.form['input'], request.form['session_id'], request.form['lang'])

def detect_intent_texts(project_id, session_id, texts, language_code):
    """Returns the result of detect intent with texts as inputs.

    Using the same `session_id` between requests allows continuation
    of the conversation."""
    
    findEnd = ""

    #explicitly use service account credentials by specifying the private key file
    storage_client = storage.Client.from_service_account_json('authentication.json')

    # make an authenticated API request
    buckets = list(storage_client.list_buckets())

    session_client = dialogflow.SessionsClient() 

    session = session_client.session_path(project_id, session_id)
    
    print("input: " + texts)
    if(language_code == "zh"):
        language_code = "zh-CN"
    
    text_input = dialogflow.TextInput(text=texts, language_code=language_code)
    print("lang: " + language_code)

    query_input = dialogflow.QueryInput(text=text_input)

    response = session_client.detect_intent(
        request={"session": session, "query_input": query_input}
    )

    print("=" * 20)
    print("Query text: {}".format(response.query_result.query_text))
    print(
        "Detected intent: {} (confidence: {})\n".format(
            response.query_result.intent.display_name,
            response.query_result.intent_detection_confidence,
        )
    )

    print("Fulfillment text: {}\n".format(response.query_result))
    return jsonify(response=response.query_result.fulfillment_text, intent=response.query_result.intent.display_name)

def RunDialogFlow(User_Input, session_id, language="en-US",):
    parser = argparse.ArgumentParser(
        description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter
    )
    parser.add_argument(
        "--project-id", help="Project/agent id.  Required.", required=True
    )
    parser.add_argument(
        "--session-id",
        help="Identifier of the DetectIntent session. " "Defaults to a random UUID."
    )
    parser.add_argument(
        "--language-code",
        help='Language code of the query. Defaults to "en-US".',
        default="en-US",
    )

    args = parser.parse_args() 

    return detect_intent_texts(
        args.project_id, session_id, User_Input, language
    )


if __name__ == "__main__":

    app.run(use_reloader=False, debug=True)

