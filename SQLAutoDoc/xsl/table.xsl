<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:param name="CurrentlyExists"></xsl:param>
<xsl:template match="/">
<html>
	<head>
		<title>TABLE: <xsl:value-of select="schema/table/@name" /></title>
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
		</style>
	</head>
	<body>
    
    <div class="Header1">
    <xsl:choose>
      <xsl:when test="$CurrentlyExists='1'">
        TABLE: <xsl:value-of select="schema/table/@name" />
      </xsl:when>
      <xsl:when test="$CurrentlyExists='0'">
        DELETED TABLE: <xsl:value-of select="schema/table/@name" />
      </xsl:when>
    </xsl:choose>
    </div>
        
<div class="Header2" colspan="6">COLUMNS</div>
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

<xsl:for-each select="schema/table/columns/column">
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

<div class="Header2">INDEXES</div>
<table>
<tr>
	<td class="ColHeader2">Name</td>
	<td class="ColHeader2">Type</td>
	<td class="ColHeader2">Unique</td>
	<td class="ColHeader2">Unique Constraint</td>
	<td class="ColHeader2">Primary Key</td>
</tr>
<xsl:for-each select="schema/table/indexes/index">
<tr>
	<td class="ColBody{position() mod 2}">
		<xsl:value-of select="@indexname" />
	</td>
	<td class="ColBody{position() mod 2}">
		<xsl:value-of select="@indextype" />
	</td>	
	<td class="ColBody{position() mod 2}">
		<xsl:value-of select="@isunique" />
	</td>	
	<td class="ColBody{position() mod 2}">
		<xsl:value-of select="@isuniqueconstraint" />
	</td>	
	<td class="ColBody{position() mod 2}">
		<xsl:value-of select="@isprimarykey" />
	</td>		
</tr>
<tr>
	<td colspan="1"></td>
	<td>
		<table>
		<xsl:for-each select="column">
		<tr>
			<td class="ColBody{position() mod 2}">
				<xsl:value-of select="@name" />
			</td>
			<td class="ColBody{position() mod 2}">
				<xsl:value-of select="@isdescendingkey" />
			</td>
		</tr>
		</xsl:for-each>
		</table>
	</td>
</tr>
</xsl:for-each>
</table>
</body>
</html>

</xsl:template>
</xsl:stylesheet>