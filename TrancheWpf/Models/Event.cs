using EventAggregator;

namespace TrancheWpf.Models
{
        public sealed class Event
        {
            #region Class Properties

            internal static Event EventInstance
            {
                get
                {
                    return eventInstance;
                }
            }

            #endregion

            #region Instance Properties

            internal IEventAggregator EventAggregator
            {
                get
                {
                    if (eventAggregator == null)
                    {
                        eventAggregator = new EventAggregator.EventAggregator();
                    }

                    return eventAggregator;
                }
            }

            #endregion

            #region Constructors

            private Event()
            {

            }

            #endregion

            #region Class Fields

            private static readonly Event eventInstance = new Event();

            #endregion

            #region Instance Fields

            private IEventAggregator eventAggregator;

            #endregion

        }
    }
