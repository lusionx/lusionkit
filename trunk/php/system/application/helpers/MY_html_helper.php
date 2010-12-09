<?php

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

function tag_js($src) {
    $reg_start = '<script src="';
    $reg_end = '" type="text/javascript"></script>';
    return $reg_start . $src . $reg_end;
}

?>
