jQuery(document).ready(function($){
	"use strict";
    
    if($('#owl-demo').length){
		$("#owl-demo").owlCarousel({
		  items : 1, //10 items above 1000px browser width
		  itemsDesktop : [1000,1], //5 items between 1000px and 901px
		  itemsDesktopSmall : [900,1], // betweem 900px and 601px
		  itemsTablet: [600,1], //2 items between 600 and 0
		  itemsMobile : false // itemsMobile disabled - inherit from itemsTablet option
		});
	}
});


