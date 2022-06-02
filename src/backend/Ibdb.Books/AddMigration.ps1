$name = $args[0]

if (!$name) {
    $name = Read-Host "Please enter migration name"
}

if (!$name) {
    Write-Error "No migration name was provided."
    return
}

dotnet ef migrations add --context Ibdb.Books.Infrastructure.Ef.BooksContext $name