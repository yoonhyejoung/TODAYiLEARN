# C# Network

### IPGlobalProperties class

provides information about the network connectivity of the local computer

##### IPGloabalProperties.GetPGlobalProperties Method

Gets an object that provides information about the local computer's network connectivity and traffic statistics.



### IPGloablStatistics class

Provides Internet Protocol statistical data.



##### IPGlobalStatistics.ForwardingEnabled Property

Get a Boolean value that specifies whether Internet Protocol packet forwarding is enabled.

```c#
public abstract bool ForwardingEnabled { get; }
```

##### IPGlobalStatistics.NumberOfInterfaces Property

Gets the number of network interfaces

```c#
public abstract int NumberOfInterfaces {get;}
```



##### IPGlobalStatistics.NumberOfIPAddress Property

Get the number of Internet Protocol(IP) address assigned to the local computer

```c#
public abstract int NumberOfIPAddresses {get; }
```

An [Int64](https://docs.microsoft.com/ko-kr/dotnet/api/system.int64?view=netframework-4.7.2) value that indicates the number of IP addresses assigned to the address family (Internet Protocol version 4 or Internet Protocol version 6) described by this object.



##### IPGlobalStatistics.NumberOfRoutes Property

Gets the number of routes in the Internet Protocol (IP) routing table

```c#
public abstract int NumberOfRoutes {get; }
```

An int64 value that specifies the total number of routes in the routing table



##### IPGlobalStatistics.DefaultTtl Property

Gets the default time-to-live(TTL) value for Internet Protocol(IP) packets.

```c#
public abstract int DefaultTtl {get; }
```



##### IPGlobalStatistics.ReceivedPacktes Property

Gets the number of Internet Protocol(IP) packets received.

```c#
public abstract long ReceviedPackets {get; }
```

An Int64 value specifies the total number of IP packet reveived.



IPGlobalStatistics.OutputPacketes Property

Gets the number of outbound Internet Protocol(IP) packets

```c#
public abstract long OutputPacktetRequests {get; }
```





