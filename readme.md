I decided to make this repo public about a month after I was done with the project. If you just want to see a demonstration of how it works without touching any of the code then you can do that by checking out this youtube video I made https://www.youtube.com/watch?v=1UrlCACQ1xs

This project was my final exam for my "Advanced C# Frameworks" course  (PROG 35142) at Sheridan College back in April of 2020. Due to the circumstances of Covid-19, the exam was administered as a take home exam. Full disclosure: students were given a week to do all this work which is good since it would be really hard to do in 3 hours. 

Now, you might be able to run the visual studio solution on your own computer but understand that it connects to a Local SQL Database and is built using a datatabase-first solution. So you will need to do two things for it to run

[Thing 1]: Execute the runThis.sql script found in the extraStuff folder. This generates all the tables and test data you need.
[Thing 2]: Change the SQL connection to the database that you used when executing runThis.sql.

If you want to know what this solution is supposed to accomplish then read PROG35142_Exam.pdf found in the extraStuff folder.

Also, the SSRS solution for Report Generation that you saw in my youtube video is not included in this repo. Just the MVC web application solution is included in this repo.