namespace homepage_backend

type MessageInput() =
    member val Message = Unchecked.defaultof<string> with get, set
    member val Mail = Unchecked.defaultof<string> with get, set
