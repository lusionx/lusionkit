<table>
    <thead>
        <tr>
        <?php foreach($thead as $e): ?>
            <th>
                <?=$e?>
            </th>
        <?php endforeach; ?>
        </tr>
    </thead>
    <tfoot>
    </tfoot>
    <tbody>
    <?php foreach($query as $row): ?>
        <tr>
        <?php foreach($row as $e1): ?>
            <td><?=$e1?></td>
        <?php endforeach; ?>
        </tr>
    <?php endforeach; ?>
    </tbody>
</table>
<div><a href="/carton/add">添加</a></div>