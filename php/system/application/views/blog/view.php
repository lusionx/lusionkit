<html>
    <head>
        <title>
            <?=$title;?>
        </title>
    </head>
    <body>    <h1>
            <?=$heading;?></h1>            <h2>blog/view</h2>
        <?php foreach($todo_list as $item):?>

            <?=$item;?>
            <br/>
    <?php endforeach;?>
</body>
</html>
