using System;
using System.Collections;
using System.Linq;
using MAUIUnitTestDemo;

namespace UnitTestProject
{
    public class ContactTestDataGenrator : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new List<ContactModal> { new ContactModal { ContactId = 1 },
                                                                 new ContactModal { ContactId = 2 }
                                                               }, 2 };
            yield return new object[] { new List<ContactModal> { new ContactModal { ContactId = 1 },
                                                                 new ContactModal { ContactId = 2 },
                                                                 new ContactModal { ContactId = 3 }
                                                               }, 3 };
            yield return new object[] {  new List<ContactModal> { new ContactModal { ContactId = 1 },
                                                                  new ContactModal { ContactId = 2 },
                                                                  new ContactModal { ContactId = 3 },
                                                                  new ContactModal { ContactId = 4 }
                                                                }, 4 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

