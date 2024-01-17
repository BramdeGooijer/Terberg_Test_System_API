#!/bin/bash

if [ -d "./TestDataApi/Migrations" ]; then
    rm -r "./TestDataApi/Migrations"
    echo "Deleted ./TestDataApi/Migrations folder."
else
    echo "./TestDataApi/Migrations folder does not exist."
fi

dotnet ef migrations add InitalCreate --project ./TestDataApi

dotnet ef database update --project ./TestDataApi  -- --environment Production

dotnet run -lp "Production" --project ./TestDataApi