# Mini free workshop 

In this workshop YOU will build the below app in 1 hour. No frameworks, pure #fsharp. 

@30th July 5PM CEST
RSVP
https://www.meetup.com/tackling-f-web-development/events/294416499/

## Fable Typing Speed demo. 

No frameworks, pure F#. 
HTML and CSS belongs to someone else.



## Build steps

Clone the project and in the root folder

``` bash
dotnet tool restore
cd src/Client
npm i
npm run start
```

Then navigate to 

```
http://localhost:8080
```


## To build for production 

``` bash
dotnet tool restore
cd src/Client
npm i
npm run build
```

deploy the `src/Client/dist` folder
