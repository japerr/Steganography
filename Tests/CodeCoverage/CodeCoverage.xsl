<?xml version="1.0"?>
<!-- http://www.codekeep.net/snippets/ae45a58b-e30b-47c7-8e3e-d6c6697dd1a2.aspx -->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

    <xsl:output method="html"/>

    <xsl:template match="/">
        <xsl:param name="highCoverage" select="90" />
        <xsl:param name="mediumCoverage" select="70" />

		<xsl:if test="//CoverageDSPriv">
			<xsl:variable name="projectName">Steganography</xsl:variable>
			<xsl:variable name="module" select="//CoverageDSPriv/Module" />
			<xsl:variable name="totalAssemblies" select="count($module)" />
			<xsl:variable name="totalFiles" select="count(//CoverageDSPriv/SourceFileNames)" />
			<xsl:variable name="totalNamespaces" select="count($module/NamespaceTable)" />
			<xsl:variable name="totalClasses" select="count($module/NamespaceTable/Class)" />
			<xsl:variable name="totalMethods" select="count($module/NamespaceTable/Class/Method)" />
			<xsl:variable name="totalBlocksCovered" select="sum($module/BlocksCovered)" />
			<xsl:variable name="totalBlocksNotCovered" select="sum($module/BlocksNotCovered)" />
			<xsl:variable name="overallCoverage" select="($totalBlocksCovered div ($totalBlocksCovered + $totalBlocksNotCovered)) * 100" />
			<html>
				<head>
					<title><xsl:value-of select="$projectName" /> Code Coverage</title>
					<style>
						html{color:#000;background:#FFF}body,div,dl,dt,dd,ul,ol,li,h1,h2,h3,h4,h5,h6,pre,code,form,fieldset,legend,input,button,textarea,select,p,blockquote,th,td{margin:0;padding:0}table{border-collapse:collapse;border-spacing:0}fieldset,img{border:0}address,button,caption,cite,code,dfn,em,input,optgroup,option,select,strong,textarea,th,var{font:inherit}del,ins{text-decoration:none}li{list-style:none}caption,th{text-align:left}h1,h2,h3,h4,h5,h6{font-size:100%;font-weight:normal}q:before,q:after{content:''}abbr,acronym{border:0;font-variant:normal}sup{vertical-align:baseline}sub{vertical-align:baseline}legend{color:#000}

						#container {margin: 0 auto;width: 100%;background: #fff;}
						#header {background: #ccc;padding: 20px;}
						#header h1 {margin: 0;font-size: 167%;text-align: center;}
						#content-container {float: left;width: 100%;background-color: #FFF;}
						#aside {float: right;width: 26%;padding: 20px 0;margin: 0 3% 0 0;display: inline;}
						#aside h3 {margin: 0;}
						#aside dt {width: 125px;text-align: right;}
						#aside dd {position: relative;top: -15px;left: 140px;width: 250px;}
						#content {clear: left;float: left;width: 60%;padding: 20px 0;margin: 0 0 0 4%;display: inline;}
						#content h2 {margin: 0;}
						#content table {font-family: "Lucida Sans Unicode", "Lucida Grande", Sans-Serif;
							font-size: 12px;background: white;width: 100%;border-collapse: collapse;text-align: left;margin: 20px;}
						#content table th {font-size: 14px;font-weight: normal;color: #039;border-bottom: 2px solid #6678B1;padding: 10px 8px;}
						#content table td {color: #669;padding: 9px 8px 0;}
						#footer {clear: left;background: #ccc;text-align: right;padding: 20px;height: 1%;}
						.tag{line-height:120%;display: block;font-size: 93%;padding: 2px 0;text-align: center;width: 100px;height: 15px;
						-webkit-border-radius: 3px;-moz-border-radius: 3px;border-radius: 3px;-webkit-box-shadow: 0 0 2px #B3B3B3;
						-moz-box-shadow: 0 0 2px #b3b3b3;box-shadow: 0 0 2px #B3B3B3;}
						.high{background-color:#86ed60;color:#3D3D3D;}
						.medium{background-color:#ffff99;color:#3D3D3D;}
						.low{background-color:#eb4848;color:#3D3D3D;}
						.centered{text-align:center;}
						body {font-family:'PT Sans', Arial, sans-serif;font-size:13px;color: #3D3D3D;}
						h1, h2, h3, h4, h5, h6, strong, b, th {font-weight:bold;}
					</style>
				</head>
				<body>
					<div id="container">
						<div id="header">
							<h1><xsl:value-of select="$projectName" /> Code Coverage Report</h1>
						</div>
						<div id="content-container">
							<div id="aside">
								<h3>Info</h3>
								<dl>
									<dt>Generated Using</dt>
									<dd>
										<p>
											<a href="http://testdriven.net/">TestDriven.NET</a>,
											MSXSL, CodeCoverage.xsl
										</p>
									</dd>
									<dt>High Coverage</dt>
									<dd>
										<span class="tag high">90% - 100%</span>
									</dd>
									<dt>Medium Coverage</dt>
									<dd>
										<span class="tag medium">70% - 89%</span>
									</dd>
									<dt>Low Coverage</dt>
									<dd>
										<span class="tag low">0% - 69%</span>
									</dd>
									<dt>Assemblies</dt>
									<dd>
										<xsl:value-of select="$totalAssemblies" />
									</dd>
									<dt>Files</dt>
									<dd>
										<xsl:value-of select="$totalFiles" />
									</dd>
									<dt>Namespaces</dt>
									<dd>
										<xsl:value-of select="$totalNamespaces" />
									</dd>
									<dt>Classes</dt>
									<dd>
										<xsl:value-of select="$totalClasses" />
									</dd>
									<dt>Methods</dt>
									<dd>
										<xsl:value-of select="$totalMethods" />
									</dd>
									<dt>Blocks Covered</dt>
									<dd>
										<xsl:value-of select="$totalBlocksCovered" />
									</dd>
									<dt>Blocks not covered</dt>
									<dd>
										<xsl:value-of select="$totalBlocksNotCovered" />
									</dd>
								</dl>
							</div>
							<div id="content">
								<!-- Build assembly data -->
								<table>
									<tr>
										<th colspan="6">
											<xsl:attribute name="class">
												centered
												<xsl:choose>
													<xsl:when test="number($overallCoverage &gt;= $highCoverage)">high</xsl:when>
													<xsl:when test="number($overallCoverage &gt;= $mediumCoverage)">medium</xsl:when>
													<xsl:otherwise>low</xsl:otherwise>
												</xsl:choose>
											</xsl:attribute>
												
											<xsl:if test="$overallCoverage > 0">
												Overall Coverage <xsl:value-of select="format-number($overallCoverage, '###.##')" />%
											</xsl:if>
											<xsl:if test="$overallCoverage = 0">
												Overall Coverage 0.00%
											</xsl:if>
										</th>
									</tr>
									<tr>
										<th>Assembly</th>
										<th>Classes</th>
										<th>Methods</th>
										<th>Blocks Covered</th>
										<th>Blocks Not Covered</th>
										<th>Coverage</th>
									</tr>

									<xsl:for-each select="$module">
										<xsl:variable name="moduleClassCount" select="count(NamespaceTable/Class)" />
										<xsl:variable name="moduleMethodCount" select="count(NamespaceTable/Class/Method)" />
										<xsl:variable name="moduleBlocksCovered" select="BlocksCovered" />
										<xsl:variable name="moduleBlocksNotCovered" select="BlocksNotCovered" />
										<xsl:variable name="moduleCoverage" select="($moduleBlocksCovered div ($moduleBlocksCovered + $moduleBlocksNotCovered)) * 100" />

										<tr>
											<td>
												<a>
													<xsl:attribute name="href">
														#<xsl:value-of select="ModuleName" />
													</xsl:attribute>
													<xsl:value-of select="ModuleName" />
												</a>
											</td>
											<td>
												<xsl:value-of select="$moduleClassCount" />
											</td>
											<td>
												<xsl:value-of select="$moduleMethodCount" />
											</td>
											<td>
												<xsl:value-of select="$moduleBlocksCovered" />
											</td>
											<td>
												<xsl:value-of select="$moduleBlocksNotCovered" />
											</td>
											<td>
												<span>
													<xsl:attribute name="class">
														tag
														<xsl:choose>
															<xsl:when test="number($moduleCoverage &gt;= $highCoverage)">high</xsl:when>
															<xsl:when test="number($moduleCoverage &gt;= $mediumCoverage)">medium</xsl:when>
															<xsl:otherwise>low</xsl:otherwise>
														</xsl:choose>
													</xsl:attribute>

													<xsl:if test="$moduleCoverage > 0">
														<xsl:value-of select="format-number($moduleCoverage, '###.##')" />%
													</xsl:if>
													<xsl:if test="$moduleCoverage = 0">
														0.00%
													</xsl:if>
												</span>
											</td>
										</tr>
									</xsl:for-each>
								</table>
								<br />
								<!-- Build class data -->
								<xsl:for-each select="$module">
									<table>
										<tr>
											<th colspan="5">
												<a>
													<xsl:attribute name="name">
														<xsl:value-of select="ModuleName" />
													</xsl:attribute>
													<xsl:value-of select="ModuleName" />
												</a>
											</th>
										</tr>
										<tr>
											<th>Class</th>
											<th>Namespace</th>
											<th>Blocks Covered</th>
											<th>Blocks Not Covered</th>
											<th>Coverage</th>
										</tr>

										<xsl:for-each select="NamespaceTable/Class">
											<xsl:variable name="classBlocksCovered" select="BlocksCovered" />
											<xsl:variable name="classBlocksNotCovered" select="BlocksNotCovered" />
											<xsl:variable name="classCoverage" select="($classBlocksCovered div ($classBlocksCovered + $classBlocksNotCovered)) * 100" />
											<xsl:variable name="classNamespaceKeyName" select="NamespaceKeyName" />

											<tr>
												<td>
													<xsl:value-of select="ClassName" />
												</td>
												<td>
													<xsl:value-of select="$module/NamespaceTable[NamespaceKeyName=$classNamespaceKeyName]/NamespaceName" />
												</td>
												<td>
													<xsl:value-of select="$classBlocksCovered" />
												</td>
												<td>
													<xsl:value-of select="$classBlocksNotCovered" />
												</td>
												<td>
													<span>
														<xsl:attribute name="class">
															tag
															<xsl:choose>
																<xsl:when test="number($classCoverage &gt;= $highCoverage)">high</xsl:when>
																<xsl:when test="number($classCoverage &gt;= $mediumCoverage)">medium</xsl:when>
																<xsl:otherwise>low</xsl:otherwise>
															</xsl:choose>
														</xsl:attribute>

														<xsl:if test="$classCoverage > 0">
															<xsl:value-of select="format-number($classCoverage, '###.##')" />%
														</xsl:if>
														<xsl:if test="$classCoverage = 0">
															0.00%
														</xsl:if>
													</span>
												</td>
											</tr>
										</xsl:for-each>
									</table>
								</xsl:for-each>
							</div>
							<div id="footer">
								Powered by Japerr.com
							</div>
						</div>
					</div>
				</body>
			</html>
		</xsl:if>
    </xsl:template>
</xsl:stylesheet>