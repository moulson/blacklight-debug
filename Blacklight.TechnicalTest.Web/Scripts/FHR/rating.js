var Rating = {
    settings: {
        // Urls
        ratingPercentageUrl: '/rating/GetRatingPercentages',
        // Elements
        authorityDropdown: $('#authorities'),
        ratingErrorMessage: $('#ratingErrorMessage'),
        ratingTableBody: $('#ratingTableBody'),
        loadingIndicator: $('#loading-indicator'),
        // Templates
        ratingTemplate: Handlebars.compile($('#rating-template').html())
    },

    // Compiled Templates

    init: function () {
        Rating.bindUI();
    },

    bindUI: function () {
        Rating.settings.authorityDropdown.changee(function () {
            Rating.settings.ratingTableBody.html('');
            Rating.ratingErrorMessageVisible(false);
            Rating.fetchRatingPercentages($(this).val());
        });
    },

    fetchRatingPercentages: function (authorityId) {
        Rating.loadingIndicatorVisible(true);
        $.ajax({
            type: 'GET',
            data: { Id: authorityId },
            url: Rating.settings.ratingPercentageUrl,
            success: function (response) {
                // Tidy the data.
                response.forEach(function (part, index, array) { // Apply precision.
                    array[index].Percentage = array[index].Percentage.toFixed(2);
                });

                Rating.settings.ratingTableBody.html(Rating.settings.ratingTemplate(response));
            },
            error: function () {
                Rating.ratingErrorMessageVisible(true);
            },
            complete: function() {
                Rating.loadingIndicatorVisible(false);
            }
        });
    },

    ratingErrorMessageVisible: function (visible) {
        if (visible) {
            Rating.settings.ratingErrorMessage.show();
        } else {
            Rating.settings.ratingErrorMessage.hide();
        }
    },

    loadingIndicatorVisible: function(visible) {
        if (visible) {
            Rating.settings.loadingIndicator.show();
        } else {
            Rating.settings.loadingIndicator.hide();
        }
    }
}