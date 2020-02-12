import { Component } from '@angular/core';
import { Globals } from '../Globals';

declare var $: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
constructor(public globals: Globals) { }
 
    ngOnDestroy(): void {
       // $('.nav-container').addClass('nav-container-fixed');
        $('.nav-container').addClass('nav-container-bg');
        $('.nav-container').removeClass('nav-container-scrolled');
    } 
 
    ngOnInit() {
      //  $('.nav-container').addClass('nav-container-fixed');
        $('.nav-container').removeClass('nav-container-bg');
        $('.nav-container').removeClass('nav-container-scrolled');

        // jQuery counterUp
        $('[data-toggle="counter-up"]').counterUp({
            delay: 10,
            time: 1000
        });
        
        // Smoth scroll on page hash links
        $('a[href*="#"]:not([href="#"],[href*="#/"],[href*="Carousel"])').on('click', function () {
            if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
                var target = $(this.hash);
                if (target.length) {
                    var top_space = 0;

                    if ($('.nav-container').length) {
                        top_space = $('.nav-container').outerHeight();

                        if (!$('.nav-container').hasClass('nav-container-scrolled')) {
                            top_space = top_space - 20;
                        }
                    }

                    $('html, body').animate({
                        scrollTop: target.offset().top - top_space
                    }, 1500, 'easeInOutExpo');

                    if ($(this).parents('.nav-menu').length) {
                        $('.nav-menu .menu-active').removeClass('menu-active');
                        $(this).closest('li').addClass('menu-active');
                    }

                    if ($('body').hasClass('mobile-nav-active')) {
                        $('body').removeClass('mobile-nav-active');
                        $('#mobile-nav-toggle i').toggleClass('fa-times fa-bars');
                        $('#mobile-body-overly').fadeOut();
                    }
                    return false;
                }
            }
        });
        
        // Porfolio filter
        $("#portfolio-flters li").click(function () {
            $("#portfolio-flters li").removeClass('filter-active');
            $(this).addClass('filter-active');

            var selectedFilter = $(this).data("filter");
            $("#portfolio-wrapper").fadeTo(100, 0);

            $(".portfolio-item").fadeOut().css('transform', 'scale(0)');

            setTimeout(function () {
                $(selectedFilter).fadeIn(100).css('transform', 'scale(1)');
                $("#portfolio-wrapper").fadeTo(300, 1);
            }, 300);
        });
         
    }
}
