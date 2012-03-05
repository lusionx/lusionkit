/* 
 * '/func/p1/p2/p3?a=122&'
 * 是 this.a 查询query
 */

exports.menu = function(p1, p2){
    return {menu:2, a:p1, b:p2, c: this.ww};
}
