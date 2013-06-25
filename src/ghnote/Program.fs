open FSharp.Net
open FSharp.Data

//HERE BE THE MAGIC!
//Type provider infering Issue type from some sample JSON in file:
//https://github.com/bentayloruk/ghnote/blob/master/src/ghnote/SampleIssues.json
type Issue = JsonProvider<"SampleIssues.json">

[<EntryPoint>]
let main argv = 
    
    try

        //Print the issue Id and Title of some bootstrap issues.

        let issues = 
            let url = "https://api.github.com/repos/twitter/bootstrap/issues"
            let headers = [ "User-Agent", "ghnote" ]
            let json = Http.Request(url, headers = headers)
            Issue.Parse(json)

        for issue in issues do
            printfn "%i: %s:" issue.Id issue.Title 
        0

    with
    | ex ->
        printfn "Yikes! %s" ex.Message
        1

