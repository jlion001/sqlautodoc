<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:param name="CurrentlyExists"></xsl:param>
  <xsl:template match="/">
<html>
	<head>
		<title>PROCEDURE: <xsl:value-of select="schema/procedure/@name" /></title>
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

  <div class="Header1">
      <xsl:choose>
        <xsl:when test="$CurrentlyExists='1'">
          PROCEDURE: <xsl:value-of select="schema/procedure/@name" />
        </xsl:when>
        <xsl:when test="$CurrentlyExists='0'">
          DELETED PROCEDURE: <xsl:value-of select="schema/procedure/@name" />
        </xsl:when>
      </xsl:choose>
    </div>
    
<div class="Header2" colspan="6">PARAMETERS</div>
<table>
<tr>
	<td class="ColHeader2">Name</td>
	<td class="ColHeader2">Type</td>
	<td class="ColHeader2">Length</td>
	<td class="ColHeader2">Scale/Precision</td>
	<td class="ColHeader2">Nullable</td>
	<td class="ColHeader2">Identity</td>
	<td class="ColHeader2">Default</td>
</tr>

<xsl:for-each select="schema/procedure/columns/column">
	<tr>
		<td class="ColBody{position() mod 2}">
			<xsl:value-of select="@colname" />
		</td>
		<td class="ColBody{position() mod 2}">
			<xsl:value-of select="@type" />
		</td>
		<td class="ColBody{position() mod 2}">
			<xsl:value-of select="@max_length" />
		</td>	
		<td class="ColBody{position() mod 2}">
			<xsl:if test="@precision &gt; 0">
				<xsl:value-of select="@scale" /> / <xsl:value-of select="@precision" />
			</xsl:if>
		</td>
		<td class="ColBody{position() mod 2}">
			<xsl:if test="@is_nullable = 1">Y</xsl:if>
		</td>	
		<td class="ColBody{position() mod 2}">
			<xsl:value-of select="@is_identity" />
		</td>			
		<td class="ColBody{position() mod 2}">
			<xsl:value-of select="@definition" />
		</td>
	</tr>
</xsl:for-each>
</table>

<div class="Header2">BODY</div>
<pre class="SQLBody">
<xsl:value-of select="schema/procedure/definition" />
</pre>
</body>
</html>

</xsl:template>
</xsl:stylesheet>