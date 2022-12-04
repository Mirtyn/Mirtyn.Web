
$(function () {

	window.setTimeout(function ()
	{
		$('.vertical-accordion').css('opacity', '1');
	}, 500);

	$('.vertical-accordion .collapse').on('click', function ()
	{
		var e = $('.vertical-accordion > div');

		if (e.hasClass('expand'))
		{
			e.removeClass('expand');

			$(this).addClass('expand');
		}
		else
		{
			$(this).addClass('expand');
		}
	})
})