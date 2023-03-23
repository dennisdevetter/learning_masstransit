$connectionString = "Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=postgres;"

if ($env:docker_connectionstring -ne $null) {
    $connectionString = $env:docker_connectionstring    
}

dotnet ef migrations add InitialCreate -- schema="lara" connectionString=$connectionString