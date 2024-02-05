# Run the following from the Windows terminal to work with sound devices:

# install dependencies:
# pip3 install uvicorn
# pip3 install FastAPI[all]
# For text to speech:
# pip3 install pyttsx3

# launch the app
# python3 -m uvicorn Pyttsx3Server:app --reload --port 11438
# browse: http://127.0.0.1:11438

from fastapi import FastAPI
from fastapi.responses import HTMLResponse
from pydantic import BaseModel
import pyttsx3
import multiprocessing

threadSpeak = None

engine = pyttsx3.init()
voices = engine.getProperty('voices') 

app = FastAPI()

class SpeakItem(BaseModel):
  sentence: str
  voice: int


@app.get("/get_voices")
async def api_get_voices():
  results = []
  index = 0
  for voice in voices:
    results.append({"name":voice.name, "id":voice.id, "index": index})
    index += 1
  return results

@app.get("/stop", response_class=HTMLResponse)
async def api_stop():
  global threadSpeak
  print (f"stop:")
  if (threadSpeak != None):
    print("stop: threadSpeak.terminate()")
    threadSpeak.terminate()
    threadSpeak = None
  return HTMLResponse(content="ok", status_code=200)

# Create a thread to iterate say() calls
def speak_in_thread(voice, sentence):
  global engine
  global voices
  #print("speak_in_thread: started")
  engine.setProperty('voice', voices[voice].id)
  engine.say(sentence)
  engine.runAndWait()
  #print("speak_in_thread: complete")

@app.post('/speak', response_class=HTMLResponse)
async def api_speak(item:SpeakItem):
  global threadSpeak

  print("speak", str(item.voice), "sentence", item.sentence)

  # stop existing utterance
  if (threadSpeak != None):
    print("stop: threadSpeak.terminate()")
    threadSpeak.terminate()
    threadSpeak = None

  # spawn thread to speak without blocking API
  threadSpeak = multiprocessing.Process(target=speak_in_thread, args=(item.voice, item.sentence))
  threadSpeak.start()
  #print("spawned speak thread")

  return HTMLResponse(content="ok", status_code=200)

print ("Python TTS Server Started")
