var fn_canvas = async(ctx,next)=>{
    ctx.render('canvas.html',{
        title :'free W'
    })
}



module.exports ={
    'GET /canvas' : fn_canvas
}