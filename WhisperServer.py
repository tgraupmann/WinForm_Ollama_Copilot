# install dependencies:
# sudo apt install uvicorn
# pip3 install FastAPI[all]
# pip3 install uvloop

# launch the app
# python -m uvicorn Server_base:app --reload
# browse: http://127.0.0.1:11437

from fastapi import FastAPI
from fastapi.responses import HTMLResponse
from pydantic import BaseModel
from typing import List
import numpy as np
import ffmpeg
import scipy.signal as sps
import whisper

print("Loading Whisper Model...")

#model = whisper.load_model("tiny")
#model = whisper.load_model("base") #max size supported by Samsung ChromeBook4 with 4GB of RAM
model = whisper.load_model("small")
#model = whisper.load_model("medium")
#model = whisper.load_model("large") # too large for my PC

def readHtml(path):
  htmlFile = open(path, mode='r') 
  htmlContent = htmlFile.read() 
  htmlFile.close()
  return htmlContent

app = FastAPI()

class TranslateItem(BaseModel):
  data: List[float]
  sampleRate: int

# hard-coded audio hyperparameters
SAMPLE_RATE = 16000

def load_audio(file: str, sr: int = SAMPLE_RATE):
  """
  Open an audio file and read as mono waveform, resampling as necessary

  Parameters
  ----------
  file: str
      The audio file to open

  sr: int
      The sample rate to resample the audio if necessary

  Returns
  -------
  A NumPy array containing the audio waveform, in float32 dtype.
  """
  try:
      # This launches a subprocess to decode audio while down-mixing and resampling as necessary.
      # Requires the ffmpeg CLI and `ffmpeg-python` package to be installed.
      out, _ = (
          ffmpeg.input(file, threads=0)
          .output("-", format="s16le", acodec="pcm_s16le", ac=1, ar=sr)
          .run(cmd=["ffmpeg", "-nostdin"], capture_stdout=True, capture_stderr=True)
      )
  except ffmpeg.Error as e:
      raise RuntimeError(f"Failed to load audio: {e.stderr.decode()}") from e

  return np.frombuffer(out, np.int16).flatten().astype(np.float32) / 32768.0

@app.post('/translate')
async def api_translate(item:TranslateItem):

  print("SAMPLE_RATE", SAMPLE_RATE, "sampleRate", item.sampleRate, "data", len(item.data))

  if len(item.data) == 0:
    return {"text":"Data is empty!"}

  #print("Transcription: Start...")
  #results = model.transcribe("Test_MP3.mp3", language="en")
  #audio = whisper.load_audio("Test_WAV.wav")
  #audio = load_audio("Test_1Sec.wav")
  #print ("ffmpeg length", len(audio), "max", max(abs(audio)))
  #print ("ffmpeg audio", audio)
  #from scipy.io import wavfile
  #rateWave,rawWave = wavfile.read("Test_WAV.wav")
  #print("rateWave", rateWave, "rawWave", rawWave)
  rawWave = item.data
  #print("sampleRate", item.sampleRate, "rawWave", rawWave)
  #print("Wave length", len(rawWave))
  #print("Wave audio", rawWave)

  # Resample data
  number_of_samples = round(len(rawWave) * float(SAMPLE_RATE) / float(item.sampleRate))
  resampledWave = sps.resample(rawWave, number_of_samples)
  #audio = np.array(resampledWave).astype(np.float32) / 32768.0
  audio = np.array(resampledWave).astype(np.float32)
  #print("Resampled length", len(audio), "max", max(abs(audio)))
  #print("Resampled audio", audio)

  audio = whisper.pad_or_trim(audio)

  # make log-Mel spectrogram and move to the same device as the model
  #mel = whisper.log_mel_spectrogram(audio).to(model.device)

  # detect the spoken language
  #_, probs = model.detect_language(mel)
  #print(f"Detected language: {max(probs, key=probs.get)}")

  # decode the audio
  #options = whisper.DecodingOptions()
  #result = whisper.decode(model, mel, options)

  # print the recognized text
  #print(result.text)

  results = model.transcribe(audio, language="en")

  #print("Transcription: Done!")
  print("Transcription: Done!", "Text", results["text"])
  return {"text":results["text"]}

@app.get("/", response_class=HTMLResponse)
async def read_items():
  print("Send: Server.html")
  return HTMLResponse(content=readHtml("Server.html"), status_code=200)

@app.get("/Server.html", response_class=HTMLResponse)
async def read_items():
  print("Send: Server.html")
  return HTMLResponse(content=readHtml("Server.html"), status_code=200)

# ref: https://blog.addpipe.com/using-recorder-js-to-capture-wav-audio-in-your-html5-web-site/

@app.get("/recorder.js", response_class=HTMLResponse)
async def read_items():
  print("Send: RecorderJS/recorder.js")
  return HTMLResponse(content=readHtml("RecorderJS/recorder.js"), status_code=200)

@app.get("/example_simple_exportwav.html", response_class=HTMLResponse)
async def read_items():
  print("Send: RecorderJS/example_simple_exportwav.html")
  return HTMLResponse(content=readHtml("RecorderJS/example_simple_exportwav.html"), status_code=200)

print("Whisper Server loaded!")
