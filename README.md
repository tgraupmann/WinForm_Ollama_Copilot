# Ollama Copilot

This application is a Windows Form application that should run on Windows and Mac.

The application has a text prompt that uses the Ollama chat API. Application windows are listed in a drop down. Ollama responses are placed into the clipboard and pasted into the selected application specified by the dropdown.

## Screenshots

![image_1](images/image_1.png)

## Dependencies

* [Ollama AI](https://ollama.ai)

* Open `WinForm_Ollama_Copilot.sln` in Visual Studio.

* The project uses Newtonsoft JSON so right-click the solution in solution explorer to select `Restore NuGet Packages`

![image_2](images/image_2.png)

* Build and run the application

![image_3](images/image_3.png)

### Ollama With Docker

* Install [Docker Desktop](https://www.docker.com/products/docker-desktop/)

```shell
docker run -d --gpus=all -v ollama:/root/.ollama -p 11434:11434 --name ollama ollama/ollama
```

* Install the `llama2` model to enable the Chat API.

```shell
docker exec -it ollama ollama run llama2
```
