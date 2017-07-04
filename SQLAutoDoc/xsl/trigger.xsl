<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/">
<html>
	<head>
		<title>TRIGGER: <xsl:value-of select="schema/trigger/@name" /></title>
		<style type="text/css">
			body {text-align: center;}
			table {border-collapse: collapse;}
			
			.Header1
			{
				font-size:14px;
				font-family:arial;
			}
			
			.Header2
			{
				font-size:12px;
				font-family:arial;
				
				padding-top: 25px;
				
				text-align: center;
			}	

			.ColHeader2
			{
				font-size:12px;
				font-family:arial;
				
				border-bottom: solid 1px black;
				
				padding-right: 10px;
			}
			
			.ColBody0
			{
				font-size:12px;
				font-family:arial;
				background-color: #CCCCCC ;		

				padding-right: 10px;
			}
			
			.ColBody1
			{
				font-size:12px;
				font-family:arial;		
				background-color: #EEEEEE;
				
				padding-right: 10px;
			}
			
			.SQLBody
			{
				text-align: left;
				font-family: courier;
			}
		</style>
	</head>
	<body>

<div class="Header1">TRIGGER: <xsl:value-of select="schema/trigger/@name" /></div>

<div class="Header2">BODY</div>
<pre class="SQLBody">
<xsl:value-of select="schema/trigger/definition" />
</pre>
</body>
</html>

</xsl:template>
</xsl:stylesheet>