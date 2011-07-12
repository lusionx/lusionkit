<?php  if ( ! defined('BASEPATH')) exit('No direct script access allowed');

function script($path)
{
    return '<script src="'.$path.'" type="text/javascript"></script>';
}

function style($path)
{
    return '<link href="'.$path.'" rel="stylesheet" type="text/css" />';
}
