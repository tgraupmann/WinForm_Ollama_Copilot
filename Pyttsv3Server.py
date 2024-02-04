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
from pydantic import BaseModel
import pyttsx3


engine = pyttsx3.init()
voices = engine.getProperty('voices') 
engine.setProperty('voice', voices[1].id)
engine.say("Python TTS Server Started")
engine.runAndWait()

app = FastAPI()

class SpeakItem(BaseModel):
  sentence: str
  voice: int

@app.post('/speak')
async def api_speak(item:SpeakItem):
  print("speak", str(item.voice), "sentence", item.sentence)

  engine = pyttsx3.init()
  voices = engine.getProperty('voices') 
  
  #male voice
  #engine.setProperty('voice', voices[0].id)  #changing index, changes voices. o for male
  #female voice
  #engine.setProperty('voice', voices[1].id)

  engine.setProperty('voice', voices[item.voice].id)

  engine.say(item.sentence)
  engine.runAndWait()
