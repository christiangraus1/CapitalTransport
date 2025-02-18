
This is my response to a coding test.

I ended up spending two hours on this, there are places I'd like to clean it up a bit but I was time poor this weekend and had to leave it as is. 
Nevertheless i think I've shown my overall approach to a task like this, using Minimal API, Automapper to convert between the entity classes and the ones tied to the API,
Newtonsoft to deal with JSON results, MOQ in my unit tests, etc.

The tests for the API are strictly speaking integration tests but I think assuming a public API and the internet are both working is OK.  I also just hard coded the token, for time, but
that's clearly something I'd ideally factor out into settings and pull from there.  

I did a helper method to strip duplicate strings, that seems appropriate to put on the List<string> class, and there's tests for that as well.

