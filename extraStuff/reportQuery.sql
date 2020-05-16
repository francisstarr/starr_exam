select b.roomNumber, b.daysBooked, r.price, r.price*b.daysBooked as revenue
from
(select roomNumber, sum(DATEDIFF(day,starting,ending)) as [daysBooked] from dbo.bookings where userName in (select userName from dbo.users where userType<>'admin') and year(starting)=year(getdate()) group by roomNumber) as b
inner join dbo.rooms as r
on (b.roomNumber=r.roomNumber)