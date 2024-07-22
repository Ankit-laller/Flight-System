alter table flightbookingtable add booked  BIT  DEFAULT (CONVERT([bit],(0))) 
truncate table flightbookingtable
select * from flightbookingtable
select * from flightbookingtable where booked = 1 order by timestamp asc
update flightbookingtable set booked = 0 where flightId= '3844ffa6-4349-4b80-ae3f-fb2687f6df71'