# RankSort-using-serial-Multi-threading-And-Threadpool-approach
Use serial, Multithreading using objects, and Multithreading using  Pool and compare the results of these three approaches.

           ----------Comparison Table-------------

N     Serialtime	MultiThreadingtime	ThreadPoolTime
100 	  0.000 s	  0.046 s	              0.031 s
500	    0.002 s	  0.053 s	              0.015 s
1000	  0.012 s	  0.082 s	              0.051 s
5000	  0.294 s	  0.635 s	              1.113 s
10000	  1.156 s	  0.858 s	              4.367 s
20000	  4.669 s	  4.617 s	              19.875 s
50000	  32.069 s	3.488 s	              1 m 59 s
100000	2m 5 s	  7.191 s	              7m 33 s


Conclusion:
->Serial time is short for small problems because multithreading overhead takes more time.
->Multithreading takes less time for relative large problem/task as seen above from 10000 onwards.
->ThreadPool class is designed for short-lived tasks, and it may not be suitable for long-running or resource-intensive tasks. In these cases, you may want to consider using the Task or Thread classes instead.

Serial
![image](https://user-images.githubusercontent.com/85791064/214760466-faa79a48-3d53-4217-a96b-94b2e1c9a9fd.png)
MultiThreading
![image](https://user-images.githubusercontent.com/85791064/214760484-91585fb8-5e92-4c0e-bdf1-93f40a2600a7.png)
ThreadPool
![image](https://user-images.githubusercontent.com/85791064/214760550-16274655-5773-468f-872a-4acfb021673f.png)

