# Install Tesseract-OCR - https://tesseract-ocr.github.io/tessdoc/Installation.html
# Add Tesseract to your path: C:\Program Files\Tesseract-OCR
# pip install pytesseract
# pip install pillow

import pytesseract
from PIL import Image

# Set the path to the image file
image_path = "images/image_8.png"

# Load the image using Pillow
img = Image.open(image_path)

# Set the language for OCR (english by default)
lang = "eng"

# Use Tesseract-OCR to recognize text in the image
text = pytesseract.image_to_string(img, lang=lang)

# Print the recognized text
print(text)
