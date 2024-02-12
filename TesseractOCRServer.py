# install dependencies:
# pip3 install uvicorn
# pip3 install FastAPI[all]
# pip install pillow
# pip install pytesseract

# Install Tesseract-OCR - https://tesseract-ocr.github.io/tessdoc/Installation.html
# Add Tesseract to your path: C:\Program Files\Tesseract-OCR

# launch the app
# python3 -m uvicorn TesseractOCRServer:app --reload --port 11439 --log-level error
# browse: http://127.0.0.1:11439

from fastapi import FastAPI
from fastapi.responses import HTMLResponse
from pydantic import BaseModel
import pytesseract
from PIL import Image
import base64
import io

app = FastAPI()

class ImageBytes(BaseModel):
  data: str

@app.post('/image_to_string')
async def image_to_string(item:ImageBytes):

    # Load the image using Pillow
    # Set the base64 string of the PNG image
    base64_string = item.data

    #print(f"image_to_string: base64_string len={len(base64_string)}")

    # Decode the base64 string into bytes
    image_bytes = base64.b64decode(base64_string)

    #print(f"image_to_string: image_bytes len={len(image_bytes)}")

    # Create a BytesIO object to load the image
    image_stream = io.BytesIO(image_bytes)

    # Open the image using Pillow
    img = Image.open(image_stream)

    # Set the language for OCR (english by default)
    lang = "eng"

    # Use Tesseract-OCR to recognize text in the image
    text = pytesseract.image_to_string(img, lang=lang)

    # Print the recognized text
    #print(text)

    return { "result": text }

print ("Python Tesseract-OCR Server Started")
