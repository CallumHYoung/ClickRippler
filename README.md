# ClickRippler

I developed this project for fun. I built this to get comfortable with C# again, it's an enjoyable language and I'm having fun using the
event-driven methods.

Total development time is ~3 hours so far.

# Features I would like to add in the future

Customizable values such as ripple size,
Choosing colors,
Probably some others that I have failed to mention in this readme. I have notes inside of the .cs file of features I would like to add. ,
Clean up the buttons. They are a little hard to click currently.

# Development Successes

I believe that the soution I developed to 'paint' the buttons to create the ripple effect is quite elegant. I initially thought of how I was
going to use a method to recursively call itself and find all buttons that were part of the current ripple, but decided that wasn't a great
way. I ran into some issues with that implementation and decided on the using a 'ring' method that you can see. The index inside of the array
of the button that is click is found, and I use some simple math to paint the ripples surrounding the clicked button.

# What I can improve on

I am quite happy of the use of the 'async' and 'await' keywords that C# provides me. My goal was to add a delay between painting the 
button and reverting the color, but other methods I tried disabled use of the UI. I assume using the async method executed in a seperate thread
which allowed the UI to remain functional. Threading is a new concept to me and I wanted to try including this into a project.

I would like to make my code more readable in the future and provide good documentation of the process to share with others.
