// jquery.tweet.js - See http://tweet.seaofclouds.com/ or https://github.com/seaofclouds/tweet for more info
// Copyright (c) 2008-2011 Todd Matthews & Steve Purcell
//(function(a){if(typeof define==="function"&&define.amd){define(["jquery"],a)}else{a(jQuery)}}(function(a){a.fn.tweet=function(d){var q=a.extend({username:null,list:null,favorites:false,query:null,avatar_size:null,count:3,fetch:null,page:1,retweets:true,intro_text:null,outro_text:null,join_text:null,auto_join_text_default:"I said,",auto_join_text_ed:"I",auto_join_text_ing:"I am",auto_join_text_reply:"I replied to",auto_join_text_url:"I was looking at",loading_text:null,refresh_interval:null,twitter_url:"twitter.com",twitter_api_url:"api.twitter.com",twitter_search_url:"search.twitter.com",template:"{avatar}{time}{join}{text}",comparator:function(r,o){return o.tweet_time-r.tweet_time},filter:function(o){return true}},d);var b=/\b((?:https?:\/\/|www\d{0,3}[.]|[a-z0-9.\-]+[.][a-z]{2,4}\/)(?:[^\s()<>]+|\(([^\s()<>]+|(\([^\s()<>]+\)))*\))+(?:\(([^\s()<>]+|(\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:'".,<>?«»????]))/gi;function m(s,t){if(typeof s==="string"){var o=s;for(var r in t){var u=t[r];o=o.replace(new RegExp("{"+r+"}","g"),u===null?"":u)}return o}else{return s(t)}}a.extend({tweet:{t:m}});function g(r,o){return function(){var s=[];this.each(function(){s.push(this.replace(r,o))});return a(s)}}function l(o){return o.replace(/</g,"&lt;").replace(/>/g,"^&gt;")}a.fn.extend({linkUser:g(/(^|[\W])@(\w+)/gi,'$1<span class="at">@</span><a href="http://'+q.twitter_url+'/$2">$2</a>'),linkHash:g(/(?:^| )[\#]+([\w\u00c0-\u00d6\u00d8-\u00f6\u00f8-\u00ff\u0600-\u06ff]+)/gi,' <a href="http://'+q.twitter_search_url+"/search?q=&tag=$1&lang=all"+((q.username&&q.username.length==1&&!q.list)?"&from="+q.username.join("%2BOR%2B"):"")+'" class="tweet_hashtag">#$1</a>'),makeHeart:g(/(&lt;)+[3]/gi,"<tt class='heart'>&#x2665;</tt>")});function f(r,o){return r.replace(b,function(u){var t=(/^[a-z]+:/i).test(u)?u:"http://"+u;var w=u;for(var v=0;v<o.length;++v){var s=o[v];if(s.url==t&&s.expanded_url){t=s.expanded_url;w=s.display_url;break}}return'<a href="'+l(t)+'">'+l(w)+"</a>"})}function k(o){return Date.parse(o.replace(/^([a-z]{3})( [a-z]{3} \d\d?)(.*)( \d{4})$/i,"$1,$2$4$3"))}function p(o){var s=function(u){return parseInt(u,10)};var r=new Date();var t=s((r.getTime()-o)/1000);if(t<1){t=0}return{days:s(t/86400),hours:s(t/3600),minutes:s(t/60),seconds:s(t)}}function c(o){if(o.days>2){return"about "+o.days+" days ago"}if(o.hours>24){return"about a day ago"}if(o.hours>2){return"about "+o.hours+" hours ago"}if(o.minutes>45){return"about an hour ago"}if(o.minutes>2){return"about "+o.minutes+" minutes ago"}if(o.seconds>1){return"about "+o.seconds+" seconds ago"}return"just now"}function h(o){if(o.match(/^(@([A-Za-z0-9-_]+)) .*/i)){return q.auto_join_text_reply}else{if(o.match(b)){return q.auto_join_text_url}else{if(o.match(/^((\w+ed)|just) .*/im)){return q.auto_join_text_ed}else{if(o.match(/^(\w*ing) .*/i)){return q.auto_join_text_ing}else{return q.auto_join_text_default}}}}}function e(){var r=("https:"==document.location.protocol?"https:":"http:");var o=(q.fetch===null)?q.count:q.fetch;var t="&include_entities=1&callback=?";if(q.list){return r+"//"+q.twitter_api_url+"/1/"+q.username[0]+"/lists/"+q.list+"/statuses.json?page="+q.page+"&per_page="+o+t}else{if(q.favorites){return r+"//"+q.twitter_api_url+"/favorites/"+q.username[0]+".json?page="+q.page+"&count="+o+t}else{if(q.query===null&&q.username.length==1){return r+"//"+q.twitter_api_url+"/1/statuses/user_timeline.json?screen_name="+q.username[0]+"&count="+o+(q.retweets?"&include_rts=1":"")+"&page="+q.page+t}else{var s=(q.query||"from:"+q.username.join(" OR from:"));return r+"//"+q.twitter_search_url+"/search.json?&q="+encodeURIComponent(s)+"&rpp="+o+"&page="+q.page+t}}}}function n(o,r){if(r){return("user" in o)?o.user.profile_image_url_https:n(o,false).replace(/^http:\/\/[a-z0-9]{1,3}\.twimg\.com\//,"https://s3.amazonaws.com/twitter_production/")}else{return o.profile_image_url||o.user.profile_image_url}}function j(r){var s={};s.item=r;s.source=r.source;s.screen_name=r.from_user||r.user.screen_name;s.name=r.from_user_name||r.user.name;s.avatar_size=q.avatar_size;s.avatar_url=n(r,(document.location.protocol==="https:"));s.retweet=typeof(r.retweeted_status)!="undefined";s.tweet_time=k(r.created_at);s.join_text=q.join_text=="auto"?h(r.text):q.join_text;s.tweet_id=r.id_str;s.twitter_base="http://"+q.twitter_url+"/";s.user_url=s.twitter_base+s.screen_name;s.tweet_url=s.user_url+"/status/"+s.tweet_id;s.reply_url=s.twitter_base+"intent/tweet?in_reply_to="+s.tweet_id;s.retweet_url=s.twitter_base+"intent/retweet?tweet_id="+s.tweet_id;s.favorite_url=s.twitter_base+"intent/favorite?tweet_id="+s.tweet_id;s.retweeted_screen_name=s.retweet&&r.retweeted_status.user.screen_name;s.tweet_relative_time=c(p(s.tweet_time));s.entities=r.entities?(r.entities.urls||[]).concat(r.entities.media||[]):[];s.tweet_raw_text=s.retweet?("RT @"+s.retweeted_screen_name+" "+r.retweeted_status.text):r.text;s.tweet_text=a([f(s.tweet_raw_text,s.entities)]).linkUser().linkHash()[0];s.tweet_text_fancy=a([s.tweet_text]).makeHeart()[0];s.time=m('<div class="icon"></div><a href="{tweet_url}" title="view tweet on twitter" class="tweetstamp">{tweet_relative_time}</a>',s);s.user=m('<a class="tweet_user" href="{user_url}">{screen_name}</a>',s);s.join=q.join_text?m(" ",s):" ";s.avatar=s.avatar_size?m("",s):"";s.text=m('<div class="tweet-text">{tweet_text_fancy}</div>',s);s.reply_action=m('<a class="tweet_action tweet_reply" href="{reply_url}">reply</a>',s);s.retweet_action=m('<a class="tweet_action tweet_retweet" href="{retweet_url}">retweet</a>',s);s.favorite_action=m('<a class="tweet_action tweet_favorite" href="{favorite_url}">favorite</a>',s);return s}function i(s){var r='<p class="tweet_intro">'+q.intro_text+"</p>";var o='<p class="tweet_outro">'+q.outro_text+"</p>";var t=a('<p class="loading">'+q.loading_text+"</p>");if(q.loading_text){a(s).not(":has(.tweet_list)").empty().append(t)}a.getJSON(e(),function(v){var u=a('<ul class="tweet_list">');var w=a.map(v.results||v,j);w=a.grep(w,q.filter).sort(q.comparator).slice(0,q.count);u.append(a.map(w,function(x){return"<li>"+m(q.template,x)+"</li>"}).join("")).children("li:first").addClass("tweet_first").end().children("li:odd").addClass("tweet_even").end().children("li:even").addClass("tweet_odd");a(s).empty().append(u);if(q.intro_text){u.before(r)}if(q.outro_text){u.after(o)}a(s).trigger("loaded").trigger((w.length===0?"empty":"full"));if(q.refresh_interval){window.setTimeout(function(){a(s).trigger("tweet:load")},1000*q.refresh_interval)}})}return this.each(function(o,r){if(q.username&&typeof(q.username)=="string"){q.username=[q.username]}a(r).unbind("tweet:load").bind("tweet:load",function(){i(r)}).trigger("tweet:load")})}}));

function setupVenn()
{
	var settings =
	{
		"topWindowBoundary": 450,
		"topWindowDeadZone": 80,
		"coeff": 0.25,
		"maxHorizontalTravel": 100
	};

	var wnd = $(window);
	var container = $("#animate-circle");
	var leftMktgCircle = $("#vennMarketing");
	var rightEngCircle = $("#vennEngineering");

	var leftCirTop = parseFloat(leftMktgCircle.css("top"));
	if (isNaN(leftCirTop))
		leftCirTop = 0;

	var rightCirTop = parseFloat(rightEngCircle.css("top"));
	if (isNaN(rightCirTop))
		rightCirTop = 0;

	wnd.on("scroll", function ()
	{
		var windowTop = wnd.scrollTop();
		var windowTopDeadMin = windowTop + settings.topWindowBoundary - settings.topWindowDeadZone;
		var windowTopDeadMax = windowTop + settings.topWindowBoundary + settings.topWindowDeadZone;
		var containerTop = container.offset().top;

		var pxToMove = 0;

		if (containerTop < windowTopDeadMin)
		{
			pxToMove = (windowTopDeadMin - containerTop) * settings.coeff;
		}
		else if (containerTop > windowTopDeadMax)
		{
			pxToMove = (containerTop - windowTopDeadMax) * settings.coeff;
		}

		if (pxToMove > settings.maxHorizontalTravel)
			pxToMove = settings.maxHorizontalTravel;
		else if (pxToMove < 0)
			pxToMove = 0;

		var opacity = -Math.abs(pxToMove / settings.maxHorizontalTravel) + 1;

		leftMktgCircle.css("top", (leftCirTop - pxToMove) + "px");
		rightEngCircle.css("top", (rightCirTop + pxToMove) + "px");

		leftMktgCircle.find(".vennText").css("opacity", opacity);
		rightEngCircle.find(".vennText").css("opacity", opacity);
	});
}

/* Parallax */
(function ($)
{
	$._parallaxSettings = 
	{
		"init": true,															// marker to know if we've already arrached a listener to the scroll event
		"topWindowBoundary": 180,											// How many pixels from the top of the window should the letters be before they're perfect
		"topWindowDeadZone": 80,											// How many pixels of wiggle room from the top window boundary should the letters be perfect
		"maxMovementPercent": 1.5,											// Percentage of the wrapper height that will be used to determine how far letters can roam,
		"elementWrapperSetupClass": "parallaxifiedWrapper",		// Marker class to find element wrappers that have been set up
		"elementWrapperOrigTop": "data-parallax-original-top",	// Attribute that stores the element wrappers's original top value
		"elementSetupClass": "parallaxified",							// Marker class to find elements that have been set up
		"elementOriginalTopAttr": "data-parallax-original-top",	// Attribute that stores the element's original top value
		"elementCoeffAttr": "data-parallax-coeff",					// Attribute that stores the element's coeff for sliding
		"elementSetup": function(element, options)
		{
			var $this = $(element);

			if(!$this.hasClass($._parallaxSettings.elementSetupClass))
			{
				var defaults = 
				{
					"start": $this.offset().top,	// Save the starting position of the letter
					"coeff": 0.5						// The multiplier of how much to move the letter
				};

				var opts = $.extend(defaults, options);

				var originalTop = parseFloat($this.css("top").replace("px", ""));

				if(isNaN(originalTop))
					originalTop = 0;

				$this.attr($._parallaxSettings.elementOriginalTopAttr, originalTop);
				$this.attr($._parallaxSettings.elementCoeffAttr, opts.coeff);

				$this.addClass($._parallaxSettings.elementSetupClass);
			}
		},
		"scrollListener": function(event)								// Listener that handles scrolling of the parallaxified elements
		{
			var wnd = $(window);

			var windowTop = wnd.scrollTop();
			var windowHeight = wnd.height();
			var maxMovement = windowHeight * $._parallaxSettings.maxMovementPercent;

			// If the original location of the letter is outside of these values, we'll move it
			var windowTopDeadMin = windowTop + $._parallaxSettings.topWindowBoundary - $._parallaxSettings.topWindowDeadZone;
			var windowTopDeadMax = windowTop + $._parallaxSettings.topWindowBoundary + $._parallaxSettings.topWindowDeadZone;

			$("." + $._parallaxSettings.elementWrapperSetupClass).each(function()
			{
				var $wrapper = $(this);

				var maxTravel = $wrapper.outerHeight() * $._parallaxSettings.maxMovementPercent;

				var originalTop = parseFloat($wrapper.attr($._parallaxSettings.elementWrapperOrigTop));
				if(isNaN(originalTop))
					originalTop = 0;

				var currentTop = parseFloat($wrapper.css("top").replace("px", ""));
				if(isNaN(currentTop))
					currentTop = 0;

				var currentOffset = $wrapper.offset().top;
				var originalOffset = currentOffset - currentTop + originalTop;

				var wrapperTooHigh = currentOffset < windowTopDeadMin;
				var wrapperTooLow = currentOffset > windowTopDeadMax;

				var pxToMove = 0;

				if(wrapperTooHigh)
				{
					pxToMove = windowTopDeadMin - originalOffset;
				}
				else if(wrapperTooLow)
				{
					pxToMove = originalOffset - windowTopDeadMax;
				}

				if(pxToMove > maxTravel)
					pxToMove = maxTravel;
				else if(pxToMove < 0)
					pxToMove = 0;

				var opacity = -Math.abs(pxToMove/maxTravel) + 1;

				$wrapper.find("." + $._parallaxSettings.elementSetupClass).each(function()
				{
					var $this = $(this);

					$this.stop(true, false);

					var coeff = parseFloat($this.attr($._parallaxSettings.elementCoeffAttr));
					if(isNaN(coeff))
						coeff = 0;

					if(pxToMove != 0)
					{
						var myPxToMove = pxToMove * coeff;
						var newTop = originalTop + myPxToMove;
						var newOffsetTop = originalOffset + myPxToMove;

						

						// Only animate if the result would be visible -- this is to keep the page more responsive
						/*if(newOffsetTop > windowTop && newOffsetTop < windowTop + windowHeight)
							$this.animate({"top": newTop + "px"}, "fast");
						else if($this.css("top") != newTop + "px")*/
							$this.css({"top": newTop + "px", "opacity": opacity});
					}
					else if($this.css("top") != originalTop + "px")
					{
						$this.closest("." + $._parallaxSettings.elementWrapperSetupClass).find("." + $._parallaxSettings.elementSetupClass).each(function()
						{
							var $this = $(this);

							var top = parseFloat($this.attr($._parallaxSettings.elementOriginalTopAttr));

							if(isNaN(top))
								top = 0;

							$this.css({"top": top + "px", "opacity": 1});
						});
						return;
					}
				});
			});
		}
	};

	// Call this function on the container of the letters
	$.fn.parallaxify = function (options)
	{
		var defaults =
		{
			"letterSelector": ".animate1",	// selector to find all the letters to slide
			"parallaxOpts": 						// Array of paralaxHelper settings.  We'll loop through each letter and cycle through the array.
			[											//		if you give it 4 settings, there will be 4 levels and every 4th letter will be the same height
				{ "coeff": 0.30 },
				{ "coeff": -0.25 },
				{ "coeff": -0.10 },
				{ "coeff": 0.45 },
				{ "coeff": -0.35 },
				{ "coeff": 0.15 }
			]
		};

		// If this is the first time being run, add the scroll listener
		if($._parallaxSettings.init)
		{
			$(window).on("scroll", $._parallaxSettings.scrollListener);
			$._parallaxSettings.init = false;
		}

		var opts = $.extend(defaults, options);

		var top = parseFloat(this.css("top").replace("px"));

		if(isNaN(top))
			top = 0;

		this.attr($._parallaxSettings.elementWrapperOrigTop, top);
		this.addClass($._parallaxSettings.elementWrapperSetupClass);

		return this.each(function()
		{
			var $this = $(this);

			// These are all the letters that we're going to slide
			var lettersToSlide = $this.find(opts.letterSelector);

			var letterIndex = 0;

			lettersToSlide.each(function ()
			{
				// make sure that if we've used the last paralaxOpt, we cycle back to the first
				if (letterIndex > opts.parallaxOpts.length)
					letterIndex = 0;

				var currOpts = opts.parallaxOpts[letterIndex];

				var letter = $(this);

				$._parallaxSettings.elementSetup(letter, currOpts);

				letterIndex++;
			});
		});
	};
})(jQuery);