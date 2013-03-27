MB Console Logger
=================

Listens to an UDP-port and prints the messages in a colored console.


Usage
-----
Add the following config to the application you want to listen to:

<configuration>
	<configSections>
		<section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler,log4net"/>
	</configSections>
	<log4net>
		<appender name="UdpAppender" type="log4net.Appender.UdpAppender">
		  <param name="RemoteAddress" value="127.0.0.1"/>
		  <param name="RemotePort" value="8082"/>
		  <layout type="log4net.Layout.PatternLayout" value="{%level}%date{MM/dd HH:mm:ss} - %message"/>
		</appender>
		<root>
		  <level value="ALL"/>
		  <appender-ref ref="UdpAppender"/>
		</root>
	</log4net>
</configuration>