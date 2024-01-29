# Ollama Copilot

This project is a Windows Form application.

The application has a text prompt that uses the Ollama chat API. Application windows are listed in a drop down. Ollama responses are placed into the clipboard and pasted into the selected application specified by the dropdown.

## Screenshots

![image_1](images/image_1.png)

## Videos

**Overview of Ollama Copilot**

<a target="_blank" href="https://www.youtube.com/watch?v=4mKgcgBTwCo"><img src="https://img.youtube.com/vi/4mKgcgBTwCo/0.jpg"/></a>

**Ollama Copilot v1.0.0**

<a target="_blank" href="https://www.youtube.com/watch?v=Jh6jCRSlclk"><img src="https://img.youtube.com/vi/Jh6jCRSlclk/0.jpg"/></a>

**Youtube Transcripts**

<a target="_blank" href="https://www.youtube.com/watch?v=lY-6ZdsuHS8"><img src="https://img.youtube.com/vi/lY-6ZdsuHS8/0.jpg"/></a>

## Dependencies

* [Ollama AI](https://ollama.ai)

* Open `WinForm_Ollama_Copilot.sln` in Visual Studio.

* The project uses Newtonsoft JSON so right-click the solution in solution explorer to select `Restore NuGet Packages`

![image_2](images/image_2.png)

* Build and run the application

![image_3](images/image_3.png)

* [Html Agility Pack](https://html-agility-pack.net/)

* [youtube-transcript-api-sharp](https://github.com/BobLd/youtube-transcript-api-sharp)

### Ollama With Docker

* Install [Docker Desktop](https://www.docker.com/products/docker-desktop/)

![image_4](images/image_4.png)

```shell
docker run -d --gpus=all -v ollama:/root/.ollama -p 11434:11434 --name ollama ollama/ollama
```

* Install the `llama2` model to enable the Chat API.

```shell
docker exec -it ollama ollama run llama2
```

* Install the `llava` model

```shell
docker exec -it ollama ollama run llava
```

* Install the `mixtral` model (requires 48GB of RAM)

```shell
docker exec -it ollama ollama run mixtral
```
