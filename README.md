# Welcome to EyesOnIt

## Computer vision - simple but powerful

**EyesOnIt** makes advanced computer vision easy for anyone without data science or computer programming skills. Even without these skills, you can apply cutting edge computer vision to your video streams and start receiving alerts by text message in just a few minutes. If you have basic programming skills, you can quickly enable endless scenarios like these:

* Motion-based detection: trigger computer vision after motion is detected
* Event recording: capture and store your own data about detections
* Image categorization: categorize a collection of images according to the contents

## You describe it, we detect it

Traditional computer vision involves a complex process to train and tune a computer vision model. This process typically requires weeks or months of time from experts with an advanced and specialized skillset. The cost for this process is often $50,000 to $100,000 USD or more. EyesOnIt is different. With EyesOnIt, you describe what you want to detect with English text. EyesOnIt compares your text to your image or video and tells you if it detected what you described. EyesOnIt does this by using the latest computer vision technology called Large Vision Models. You can download and try the full version of EyesOnIt for free with a developer license, and the production license cost is very affordable. You can also try our free demo for yourself [here](https://www.eyesonit.us/free-demo-sign-up).

## Let's get started

### Quick start

#### 1. Create an account and get your free developer license
Create an account or log in by clicking the Log In / Sign Up link above

Go to the account page to get your license key and token


#### 2. Install the software
* Install Docker from [here](https://docs.docker.com/engine/install/)
* Run Docker
* Pull our Docker image

    ```
    docker pull eyesonit/eyesonit_v2
    ```

#### 3. Run the software
* Create a container with your license key and token as parameters

    ##### If you have an NVIDIA GPU
    
    Use this Docker run command to create your container with GPU support:

    ```
    docker run -d -p 8000:8000 --gpus all -e EOI_LICENSE_KEY='<your license key>' -e EOI_AUTHORIZATION_TOKEN='<your license token>' -v <host-docker-volume-path>:/home/eyesonit_data eyesonit/eyesonit_v1:latest
    ```

    ##### If you don't have an NVIDIA GPU
    
    You can use your CPU for processing with this command:

    ```
    docker run -d -p 8000:8000 -e EOI_LICENSE_KEY='<your license key>' -e EOI_AUTHORIZATION_TOKEN='<your license token>' -v <host-docker-volume-path>:/home/eyesonit_data eyesonit/eyesonit_v1:latest
    ```

*Note*: If you are receiving your RTSP stream through a video management system, you may have to open another port on the container. You can do that by adding something like "-p 654:654" after "-p 8000:8000".

* Open the test UI

    On the same computer where you ran EyesOnIt, open a browser and navigate to http://localhost:8000/dashboard

    You can find more information about the test UI [here](/docs/Trying%20EyesOnIt/index.md)
