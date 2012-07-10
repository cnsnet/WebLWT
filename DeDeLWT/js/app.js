$(window).resize(function () {
  slideSpeed = 200;
  productsOffset = $("#products").offset().top;
  aboutOffset = $("#about").offset().top;
  peopleOffset = $("#people").offset().top;
  honourOffset = $("#honour").offset().top;
  cultureOffset = $("#culture").offset().top;
  jobsOffset = $("#jobs").offset().top;
  blogOffset = $("#blog").offset().top;
});

$(document).ready(function () {
  $(window).resize();
  // Identify if visitor on mobile with lame sniffing to remove parallaxing title
  var isMobile;
  if (navigator.userAgent.match(/Android/i) || navigator.userAgent.match(/webOS/i) || navigator.userAgent.match(/iPhone/i) || navigator.userAgent.match(/iPod/i) || navigator.userAgent.match(/iPad/i) || navigator.userAgent.match(/BlackBerry/)) {
    isMobile = true;
  }
  /* Disable Clicks */
  $("#btn-contact, .btn-products-more, .btn-cutting-room, nav a, h1").click(function () {
    return false;
  });
  /* Footer Buttons */
  $("h1").click(function () {
    $('html,body').animate({
      scrollTop: 0
    }, slideSpeed);
  });
  /* Footer Buttons */
  $("footer").click(function () {
    $(this).toggleClass("active");
    $("#info").stop().slideToggle(slideSpeed);
  });
  $(".product").hover(

  function () {
    $(this).find('.more').stop(true, false).slideToggle(slideSpeed);
  });
  $(".btn-products-more").click(function () {
    $(this).toggleClass("active");
    $("#season2").slideToggle(slideSpeed);
    $(".btn-cutting-room, #products-control .line").toggle();

    var currentText = $(this).html();
    if (currentText == "see more products") {
      $(this).empty().append("see less products");
    } else {
      if ($(".btn-cutting-room").hasClass("active")) {
        $(".btn-cutting-room").click();
      }
      $(this).empty().append("see more products");
    }
    /* Recalculate offsets */
    aboutOffset = $("#about").offset().top;
    peopleOffset = $("#people").offset().top;
    jobsOffset = $("#jobs").offset().top;
    blogOffset = $("#blog").offset().top;
  });
  $(".btn-cutting-room").click(function () {
    $("#cutting-room, #cutting-btm").slideToggle(slideSpeed);
    $(this).toggleClass("active");
  });
  /* Person */
  $(".person").hover(

  function () {
    $(this).find('.bio').stop(true, false).slideToggle(slideSpeed);
  });
  /* Scroll on Nav Click */
  $("nav a").click(function () {
    var scrollID = jQuery(this).attr("id").substr(4);
    var targetOffset = $("#" + scrollID).offset().top;
    targetOffset = targetOffset; /* Add padding for Header */
    $('html,body').animate({
      scrollTop: targetOffset
    }, slideSpeed);
  });
  if (!isMobile) {
    $(window).scroll(function () {
      slidingTitle();
    });
  }
  /* Call Parallax */
  function slidingTitle() {
    //Get scroll position of window
    windowScroll = $(this).scrollTop() + 5;

    /* Products */
    if (windowScroll < aboutOffset) {
      $("nav a").removeClass("active");
      $("#nav-products").toggleClass("active");
    }

    /* About */
    if ((windowScroll >= aboutOffset) && (windowScroll < peopleOffset)) {
      $("nav a").removeClass("active");
      $("#nav-about").toggleClass("active");
    }

    /* People */
    if ((windowScroll >= peopleOffset) && (windowScroll < honourOffset)) {
      $("nav a").removeClass("active");
      $("#nav-people").toggleClass("active");
    }

    /* Honour */
    if ((windowScroll >= honourOffset) && (windowScroll < cultureOffset)) {
      $("nav a").removeClass("active");
      $("#nav-honour").toggleClass("active");
    }

    /* Culture */
    if ((windowScroll >= cultureOffset) && (windowScroll < jobsOffset)) {
      $("nav a").removeClass("active");
      $("#nav-culture").toggleClass("active");
    }
    
    /* Jobs */
    if ((windowScroll >= jobsOffset) && (windowScroll < blogOffset)) {
      $("nav a").removeClass("active");
      $("#nav-jobs").toggleClass("active");
      footerFix();
    }

    /* Jobs */
    if ((windowScroll >= blogOffset)) {
      $("nav a").removeClass("active");
      $("#nav-blog").toggleClass("active");
      footerShow();
    }
  }

  function footerFix() {
    $("#btn-contact").removeClass("active");
    $("footer").css("position", "fixed");
    $("#info").hide();
  }

  function footerShow() {
    $("#btn-contact").addClass("active");
    $("footer").css("position", "relative");
    $("#info").show();
  }

  $("#animate-products").parallaxify();
  $("#animate-people").parallaxify({
    "letterSelector": ".animate3"
  });
  $("#animate-about").parallaxify();
  $("#animate-honour").parallaxify({
    "letterSelector": ".animate6"
  });
  $("#animate-culture").parallaxify({
    "letterSelector": ".animate7"
  });
  $("#animate-jobs").parallaxify({
    "letterSelector": ".animate4"
  });
  $("#animate-blog").parallaxify({
    "letterSelector": ".animate5"
  });

  $._parallaxSettings.scrollListener();
  setupVenn();

  $("a[rel^='prettyPhoto']").prettyPhoto({ theme: 'facebook' });

  $('#wall').jmFullWall({
    itemsForRow: 6
  });

  /* Get Tumblr Feed */
  //  $.getJSON("http://dedegroup.tumblr.com/api/read/json?callback=?", function (data) {
  //    $.each(data.posts, function (i, posts) {
  //      var title = this["regular-title"] || this["link-text"];
  //      var type = this.type;
  //      var date = this.date.substr(0, 16);
  //      var url = this["url-with-slug"];
  //      var text = this["regular-body"] || this["link-description"];
  //      text = text.replace(/<\/?[^>]+>/gi, '').substr(0, 180);
  //      $('#tumblr').append('<div class="post"><a href=' + url + ' target="_blank"><span>' + date + '</span><h4>' + title + '</h4></a><p>' + text + '.... <a href="' + url + '" target="_top">Read more.</a></p></div>');
  //    });
  //  });

  /* Twitter */
  //	$(function ($)
  //	{
  //		$(".tweet").tweet({
  //			join_text: "auto",
  //			username: "dedegroup",
  //			avatar_size: 48,
  //			count: 1,
  //			auto_join_text_default: "we said,",
  //			auto_join_text_ed: "we",
  //			auto_join_text_ing: "we were",
  //			auto_join_text_reply: "we replied",
  //			auto_join_text_url: "we were checking out",
  //			loading_text: "loading tweets..."
  //		});
  //	});
});