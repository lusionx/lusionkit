<?php echo form_open('carton/add')?>
<table>
<tbody>
<tr><td>name</td><td><input name="name" type="text"/></td></tr>
<tr><td>name_en</td><td><input name="name_en" type="text"/></td></tr>
<tr><td>last</td><td><input name="last" type="text"/></td></tr>
<tr><td>episode</td><td><input name="episode" type="text"/></td></tr>
<tr><td>statr</td><td><input name="statr" type="text"/></td></tr>
<tr><td>remark</td><td><input name="remark" type="text"/></td></tr>
<tr><td>labels</td><td><input name="labels" type="text"/></td></tr>
<tr><td>link</td><td><input name="link" type="text"/></td></tr>
<tr><td>update</td><td><input name="update" type="text"/></td></tr>
</tbody>
<tfoot>
<tr>
<td></td><td><input type="submit" value="保存"></td>
</tr>
</tfoot>
</table>
<?php echo form_close()?>