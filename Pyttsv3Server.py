# Run the following from the Windows terminal to work with sound devices:

# install dependencies:
# pip3 install uvicorn
# pip3 install FastAPI[all]
# For text to speech:
# pip3 install pyttsx3

# launch the app
# python3 -m uvicorn Pyttsv3Server:app --reload --port 11438
# browse: http://127.0.0.1:11438

from fastapi import FastAPI
from fastapi.responses import HTMLResponse
from pydantic import BaseModel
import pyttsx3
import threading
import time

pendingSpeak = False

def onWordSpeak(name, location, length):
  global engine
  global pendingSpeak
  print(f"onWordSpeak: pendingSpeak={pendingSpeak} name={name}")
  if (not pendingSpeak):
    engine.stop()

def onEndSpeak(name, completed):
  global pendingSpeak
  print("onEndSpeak", name, completed)
  pendingSpeak = False

engine = pyttsx3.init()
engine.connect('finished-utterance', onEndSpeak)
engine.connect('started-word', onWordSpeak)
voices = engine.getProperty('voices') 
engine.setProperty('voice', voices[1].id)
engine.say("Python TTS Server Started")
engine.runAndWait()

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
  global pendingSpeak
  print (f"stop: pendingSpeak={pendingSpeak}")
  pendingSpeak = False
  return HTMLResponse(content="ok", status_code=200)

@app.post('/speak')
async def api_speak(item:SpeakItem):
  global pendingSpeak
  global engine
  print("speak", str(item.voice), "sentence", item.sentence)
  pendingSpeak = True
  engine.setProperty('voice', voices[item.voice].id)
  engine.say(item.sentence)
  print("speak done")

print ("Python TTS Server Started")

# Create a thread to iterate say() calls
def speak_in_thread():
  global engine
  global pendingSpeak
  print("speak_in_thread: started")
  #engine.connect('started-word', onWordSpeak)
  #engine.runAndWait()
  while (True):
    time.sleep(0.1)
    #print("speak_in_thread: startLoop()")
    engine.startLoop(False)
    #print("speak_in_thread: iterate()")
    engine.iterate()
    #print("speak_in_thread: endLoop()")
    engine.endLoop()
threadSpeak = threading.Thread(target=speak_in_thread)
threadSpeak.start()

# Create a thread to iterate engine.stop() calls
def stop_in_thread():
  global engine
  global pendingSpeak
  print("stop_in_thread: started")
  while (True):
    time.sleep(0.1)
    if (not pendingSpeak):
      pendingSpeak = True
      print("stop_in_thread: engine.stop()")
      engine.stop()
threadStop = threading.Thread(target=stop_in_thread)
threadStop.start()
