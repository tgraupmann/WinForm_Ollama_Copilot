using System;
using System.Collections.Generic;

namespace YoutubeTranscriptApi
{
    //https://github.com/jdepoix/youtube-transcript-api/blob/master/youtube_transcript_api/_errors.py

    /// <summary>
    /// Raised if a transcript could not be retrieved.
    /// </summary>
    public class CouldNotRetrieveTranscript : Exception
    {
        public string VideoId { get; }

        private const string ERROR_MESSAGE = "\nCould not retrieve a transcript for the video {0}!";
        private const string CAUSE_MESSAGE_INTRO = " This is most likely caused by:\n\n{0}";
        private const string GITHUB_REFERRAL = (
        "\n\nIf you are sure that the described cause is not responsible for this error " +
        "and that a transcript should be retrievable, please create an issue at " +
        "https://github.com/jdepoix/youtube-transcript-api/issues. " +
        "Please add which version of youtube_transcript_api you are using " +
        "and provide the information needed to replicate the error. " +
        "Also make sure that there are no open issues which already describe your problem!");

        /// <summary>
        /// Initializes a new instance of the <see cref="CouldNotRetrieveTranscript"/> class.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="cause"></param>
        protected CouldNotRetrieveTranscript(string videoId, string cause)
            : base(buildErrorMessage(videoId, cause))
        {
            VideoId = videoId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CouldNotRetrieveTranscript"/> class.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="cause"></param>
        /// <param name="exception"></param>
        public CouldNotRetrieveTranscript(string videoId, string cause, Exception exception)
            : base(buildErrorMessage(videoId, cause), exception)
        { }

        private static string buildErrorMessage(string videoId, string cause)
        {
            string error_message = string.Format(ERROR_MESSAGE, string.Format(Settings.WATCH_URL, videoId));

            if (!string.IsNullOrEmpty(cause))
            {
                error_message += string.Format(CAUSE_MESSAGE_INTRO, cause) + GITHUB_REFERRAL;
            }

            return error_message;
        }
    }

    /// <inheritdoc/>
    public sealed class VideoUnavailable : CouldNotRetrieveTranscript
    {
        private const string CAUSE_MESSAGE = "The video is no longer available";

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoUnavailable"/> class.
        /// </summary>
        /// <param name="videoId"></param>
        public VideoUnavailable(string videoId) : base(videoId, CAUSE_MESSAGE)
        { }
    }

    /// <inheritdoc/>
    public sealed class TooManyRequests : CouldNotRetrieveTranscript
    {
        private const string CAUSE_MESSAGE =
            "YouTube is receiving too many requests from this IP and now requires solving a captcha to continue. " +
            "One of the following things can be done to work around this:\n" +
            "- Manually solve the captcha in a browser and export the cookie. " +
            "Read here how to use that cookie with " +
            "youtube-transcript-api: https://github.com/jdepoix/youtube-transcript-api#cookies\n" +
            "- Use a different IP address\n" +
            "- Wait until the ban on your IP has been lifted";

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyRequests"/> class.
        /// </summary>
        /// <param name="videoId"></param>
        public TooManyRequests(string videoId) : base(videoId, CAUSE_MESSAGE)
        { }
    }

    /// <inheritdoc/>
    public sealed class TranscriptsDisabled : CouldNotRetrieveTranscript
    {
        private const string CAUSE_MESSAGE = "Subtitles are disabled for this video";

        /// <summary>
        /// Initializes a new instance of the <see cref="TranscriptsDisabled"/> class.
        /// </summary>
        /// <param name="videoId"></param>
        public TranscriptsDisabled(string videoId) : base(videoId, CAUSE_MESSAGE)
        { }
    }

    /// <inheritdoc/>
    public sealed class NoTranscriptAvailable : CouldNotRetrieveTranscript
    {
        private const string CAUSE_MESSAGE = "No transcripts are available for this video";

        /// <summary>
        /// Initializes a new instance of the <see cref="NoTranscriptAvailable"/> class.
        /// </summary>
        /// <param name="videoId"></param>
        public NoTranscriptAvailable(string videoId) : base(videoId, CAUSE_MESSAGE)
        { }
    }

    /// <inheritdoc/>
    public sealed class NotTranslatable : CouldNotRetrieveTranscript
    {
        private const string CAUSE_MESSAGE = "The requested language is not translatable";

        /// <summary>
        /// Initializes a new instance of the <see cref="NotTranslatable"/> class.
        /// </summary>
        /// <param name="videoId"></param>
        public NotTranslatable(string videoId) : base(videoId, CAUSE_MESSAGE)
        { }
    }

    /// <inheritdoc/>
    public sealed class TranslationLanguageNotAvailable : CouldNotRetrieveTranscript
    {
        private const string CAUSE_MESSAGE = "The requested translation language is not available";

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationLanguageNotAvailable"/> class.
        /// </summary>
        /// <param name="videoId"></param>
        public TranslationLanguageNotAvailable(string videoId) : base(videoId, CAUSE_MESSAGE)
        { }
    }

    /// <inheritdoc/>
    public sealed class CookiePathInvalid : CouldNotRetrieveTranscript
    {
        private const string CAUSE_MESSAGE = "The provided cookie file was unable to be loaded";

        /// <summary>
        /// Initializes a new instance of the <see cref="CookiePathInvalid"/> class.
        /// </summary>
        /// <param name="videoId"></param>
        public CookiePathInvalid(string videoId) : base(videoId, CAUSE_MESSAGE)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CookiePathInvalid"/> class.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="exception"></param>
        public CookiePathInvalid(string videoId, Exception exception) : base(videoId, CAUSE_MESSAGE, exception)
        { }
    }

    /// <inheritdoc/>
    public sealed class CookiesInvalid : CouldNotRetrieveTranscript
    {
        private const string CAUSE_MESSAGE = "The cookies provided are not valid (may have expired)";

        /// <summary>
        /// Initializes a new instance of the <see cref="CookiesInvalid"/> class.
        /// </summary>
        /// <param name="videoId"></param>
        public CookiesInvalid(string videoId) : base(videoId, CAUSE_MESSAGE)
        { }
    }

    /// <inheritdoc/>
    public sealed class FailedToCreateConsentCookie : CouldNotRetrieveTranscript
    {
        private const string CAUSE_MESSAGE = "Failed to automatically give consent to saving cookies";

        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToCreateConsentCookie"/> class.
        /// </summary>
        /// <param name="videoId"></param>
        public FailedToCreateConsentCookie(string videoId) : base(videoId, CAUSE_MESSAGE)
        { }
    }

    /// <inheritdoc/>
    public sealed class NoTranscriptFound : CouldNotRetrieveTranscript
    {
        private const string CAUSE_MESSAGE = "No transcripts were found for any of the requested language codes: {0}\n\n{1}";

        /// <summary>
        /// Initializes a new instance of the <see cref="NoTranscriptFound"/> class.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="requestedLanguageCodes"></param>
        /// <param name="transcriptData"></param>
        public NoTranscriptFound(string videoId, IReadOnlyList<string> requestedLanguageCodes, TranscriptList transcriptData)
            : base(videoId, CAUSE_MESSAGE)
        {
            // TODO
        }
    }
}
