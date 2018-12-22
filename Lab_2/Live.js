var LiveApp = {
    game_field:undefined, size: 90, generation:undefined, is_game_over:false, timer:undefined, time_interval: 500
};

LiveApp.GetNumberOfNeighbours = function(gen, y, x){
    var neighbours_num = -gen[y][x];
    for(var  i = y-1; i <= y+1; i+=1)
    {
        if(gen[i] !== undefined)
            for(var  j = x-1; j <=x+1; j+=1)
                if(typeof gen[i][j] === 'number')
                    neighbours_num += gen[i][j];
    }
    return neighbours_num;
}

LiveApp.ResizeField = function(new_size) {
    this.size = new_size;
    this.InitializeApp();
}

LiveApp.ChangeIntervalBetweenGenerations = function(delta){
    this.time_interval = delta;
}

LiveApp.RedrawField = function(){
    var k = (this.game_field.parentNode.clientWidth > 800) ? 0.4 : 1;
    this.game_field.height = this.game_field.width = this.game_field.parentNode.clientWidth*k;
    var deltaX = this.game_field.width/LiveApp.size;
    var deltaY = this.game_field.height/LiveApp.size;
    for (y = 0; y < this.size; y += 1) {
        for (x = 0; x < this.size; x += 1) {
            if(this.generation[y][x] === 0)
                this.game_field.getContext("2d").strokeRect(x*deltaX, y*deltaY, deltaX, deltaY)
            else
                this.game_field.getContext("2d").fillRect(x*deltaX, y*deltaY, deltaX, deltaY);
        }
    }
}

LiveApp.InitializeApp = function(game_field){
    this.game_field = game_field;
    this.generation = [];
    for (var y = 0; y < this.size; y+=1){
	    this.generation[y] = [];
	    for (var x = 0; x < this.size; x+=1){
		    this.generation[y][x] = 0;
        }
    }
    this.RedrawField();
    this.is_game_over = false;
    this.clickListener = function (e) {
        var left = e.pageX - e.target.offsetLeft,
            top = e.pageY - e.target.offsetTop;
        var deltaY = LiveApp.game_field.height/LiveApp.size,
            deltaX = LiveApp.game_field.width/LiveApp.size;
        var x = Math.floor(left/deltaX),
            y = Math.floor(top/deltaY);
        LiveApp.generation[y][x] = (LiveApp.generation[y][x]===0) ? 1 : 0;
        LiveApp.RedrawField(); 
    }
    this.game_field.addEventListener('click', this.clickListener) 
}

LiveApp.ProcessNextGeneration = function(){
    var prev_gen = JSON.parse(JSON.stringify(this.generation));
    this.is_game_over = true;
    for (var y = 0; y < this.size; y+=1){
	    for (var x = 0; x < this.size; x+=1){
            var neighbours_num = this.GetNumberOfNeighbours(prev_gen, y, x);
            if(prev_gen[y][x]===1 && neighbours_num < 2 || neighbours_num > 3)
                this.generation[y][x] = 0;
            else if(neighbours_num === 3 && prev_gen[y][x]===0)
                this.generation[y][x] = 1;
            if(this.is_game_over && prev_gen[y][x] !== this.generation[y][x])
                this.is_game_over = false;
        }
    }
}

window.onload = function() {
    var canvas = document.getElementById("game-field");
    var countNode = document.getElementById("count");
    canvas.height = canvas.width = 0;
    LiveApp.InitializeApp(canvas);
    var startButton = document.getElementById("start-game");
    var restartButton = document.getElementById("restart-game");
    startButton.addEventListener('click',function () {
        var count=0;
        LiveApp.game_field.removeEventListener('click',LiveApp.clickListener);
        this.style.display = "none";
        restartButton.style.display = "block";
        LiveApp.timer = setInterval(function (){
            LiveApp.ProcessNextGeneration();
            if(LiveApp.is_game_over)
                clearInterval(LiveApp.timer);
            else
                count+=1;
            LiveApp.RedrawField();
            countNode.innerText = count.toString();
        }, LiveApp.time_interval);
            
    }) 
    restartButton.addEventListener('click',function () {
        if(!LiveApp.is_game_over)
            clearInterval(LiveApp.timer);
        LiveApp.InitializeApp(canvas);
        this.style.display = "none";
        startButton.style.display = "block";
        countNode.innerText = '0';
    })
    setInterval(function(){
        if(LiveApp.game_field.width !== LiveApp.game_field.parentNode.clientWidth)
        {
            LiveApp.game_field.parentNode.style.height = LiveApp.game_field.parentNode.clientWidth;
            LiveApp.RedrawField();
        }
    }, 200);
}
