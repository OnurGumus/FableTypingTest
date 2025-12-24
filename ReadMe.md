# Fable Typing Speed Test

A typing speed test built with F#, Fable, Lit, and Elmish.

## Live Demo

https://zealous-field-0fa61df1e.3.azurestaticapps.net/

## Build steps

Clone the project and in the root folder:

``` bash
dotnet tool restore
cd src/Client
npm i
npm run start
```

Then navigate to http://localhost:5173/

## To build for production

``` bash
dotnet tool restore
cd src/Client
npm i
npm run build
```

Deploy the `src/Client/dist` folder.
