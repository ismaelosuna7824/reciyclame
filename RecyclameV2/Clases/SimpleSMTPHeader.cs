using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class SimpleSMTPHeader
    {
        private String __subject, __from;
        private StringBuilder __to;
        private StringBuilder __headerFields;
        private StringBuilder __cc;
        private StringBuilder __bcc;
        private StringBuilder __date;
        private StringBuilder __contentype;
        private StringBuilder __mime;
        private StringBuilder __messageid;
        private bool __isAppend = false;

        /***
     * Creates a new SimpleSMTPHeader instance initialized with the given
     * from, to, and subject header field values.
     * <p>
     * @param from  The value of the <code>From:</code> header field.  This
     *              should be the sender's email address.
     * @param to    The value of the <code>To:</code> header field.  This
     *              should be the recipient's email address.
     * @param subject  The value of the <code>Subject:</code> header field.
     *              This should be the subject of the message.
     ***/
        public SimpleSMTPHeader(String from, String to, String subject)
        {
            __to = new StringBuilder(to);
            __from = from;
            __subject = subject;
            __headerFields = new StringBuilder();
            __cc = null;
            __bcc = null;
            __mime = null;
            __date = null;
            __messageid = null;
            __contentype = null;
        }

        public SimpleSMTPHeader(String from, String subject)
        {
            __to = null;
            __from = from;
            __subject = subject;
            __headerFields = new StringBuilder();
            __cc = null;
            __bcc = null;
            __mime = null;
            __date = null;
            __messageid = null;
            __contentype = null;
        }

        public void isAppend(bool isappend)
        {
            __isAppend = isappend;
        }
        /***
         * Adds an arbitrary header field with the given value to the article
         * header.  These headers will be written before the From, To, Subject, and
         * Cc fields when the SimpleSMTPHeader is convertered to a string.
         * An example use would be:
         * <pre>
         * header.addHeaderField("Organization", "Foobar, Inc.");
         * </pre>
         * <p>
         * @param headerField  The header field to add, not including the colon.
         * @param value  The value of the added header field.
         ***/
        public void addHeaderField(String headerField, String value)
        {
            __headerFields.Append(headerField);
            __headerFields.Append(": ");
            __headerFields.Append(value);
            __headerFields.Append("\r\n");
        }

        public void addBoundaryField(String value)
        {
            __headerFields.Append("------");
            __headerFields.Append(value);
            __headerFields.Append("\r\n");
        }

        public void addBoundaryGmailApiField(String value)
        {
            __headerFields.Append("--");
            __headerFields.Append(value);
            __headerFields.Append("\r\n");
        }
        /***
         * Add an email address to the CC (carbon copy or courtesy copy) list.
         * <p>
         * @param address The email address to add to the CC list.
         ***/
        public void addCC(String address)
        {
            if (__cc == null)
            {
                __cc = new StringBuilder();
            }
            else
            {
                __cc.Append(", ");
            }

            __cc.Append(address);
        }

        /***
         * Add an date created email.
         * <p>
         * @param Date The email to add to the sent Date.
         ***/
        public void addDate(String date)
        {
            if (__date == null)
            {
                __date = new StringBuilder();
            }
            else
            {
                __date.Append(", ");
            }

            __date.Append(date);
        }

        /***
         * Add an Mime version email.
         * <p>
         * @param Mime version The email.
         ***/
        public void addMime(String mime)
        {
            if (__mime == null)
            {
                __mime = new StringBuilder();
            }
            else
            {
                __mime.Append(", ");
            }

            __mime.Append(mime);
        }

        /***
         * Add an ContenType of email body.
         * <p>
         * @param ContenType The email.
         ***/
        public void addContenType(String contentype)
        {
            if (__contentype == null)
            {
                __contentype = new StringBuilder();
            }
            else
            {
                __contentype.Append(", ");
            }

            __contentype.Append(contentype);
        }

        /***
         * Add an MessageId of email body.
         * <p>
         * @param MessageId The email.
         ***/
        public void addMessageId(String messageid)
        {
            if (__messageid == null)
            {
                __messageid = new StringBuilder();
            }
            else
            {
                __messageid.Append(", ");
            }

            __messageid.Append(messageid);
        }
        /***
         * Add an email address to the BCC (carbon copy or courtesy copy) list.
         * <p>
         * @param address The email address to add to the CC list.
         ***/
        public void addBCC(String address)
        {
            if (__bcc == null)
            {
                __bcc = new StringBuilder();
            }
            else
            {
                __bcc.Append(", ");
            }

            __bcc.Append(address);
        }
        /***
         * Add an email address to the CC (carbon copy or courtesy copy) list.
         * <p>
         * @param address The email address to add to the CC list.
         ***/
        public void addTO(String address)
        {
            if (__to == null)
            {
                __to = new StringBuilder();
            }
            else
            {
                __to.Append(", ");
            }

            __to.Append(address);
        }
        /***
         * Converts the SimpleSMTPHeader to a properly formatted header in
         * the form of a String, including the blank line used to separate
         * the header from the article body.  The header fields CC and Subject
         * are only included when they are non-null.
         * <p>
         * @return The message header in the form of a String.
         ***/

        public String toString()
        {
            StringBuilder header = new StringBuilder();

            if (__headerFields.Length > 0)
            {
                header.Append(__headerFields.ToString());
            }
            bool clrf = true;
            if (__from != null)
            {
                header.Append("From: ");
                header.Append(__from);
            }
            else
            {
                clrf = false;
            }
            if (__to != null)
            {
                if (clrf)
                {
                    header.Append("\r\nTo: ");
                }
                else
                {
                    header.Append("To: ");
                }
                header.Append(__to);
                clrf = true;
            }

            if (__cc != null)
            {
                if (clrf)
                {
                    header.Append("\r\nCc: ");
                }
                else
                {
                    header.Append("Cc: ");
                }
                header.Append(__cc.ToString());
                clrf = true;
            }

            if (__subject != null)
            {
                if (clrf)
                {
                    header.Append("\r\nSubject: ");
                }
                else
                {
                    header.Append("Subject: ");
                }
                header.Append(__subject);
                clrf = true;
            }

            if (__isAppend && __bcc != null)
            {
                if (clrf)
                {
                    header.Append("\r\nBcc: ");
                }
                else
                {
                    header.Append("Bcc: ");
                }
                header.Append(__bcc.ToString());
                clrf = true;
            }
            header.Append("\r\n");
            header.Append("\r\n");

            return header.ToString();
        }

        public String toDraftString()
        {
            StringBuilder header = new StringBuilder();

            bool clrf = true;
            if (__date != null)
            {
                header.Append("Date: ");
                header.Append(__date);
            }
            else
            {
                clrf = false;
            }
            if (__from != null)
            {
                if (clrf)
                {
                    header.Append("\r\nFrom: ");
                }
                else
                {
                    header.Append("From: ");
                }
                header.Append(__from);
                clrf = true;
            }
            else
            {
                clrf = false;
            }
            if (__subject != null)
            {
                if (clrf)
                {
                    header.Append("\r\nSubject: ");
                }
                else
                {
                    header.Append("Subject: ");
                }
                header.Append(__subject);
                clrf = true;
            }
            if (__to != null)
            {
                if (clrf)
                {
                    header.Append("\r\nTo: ");
                }
                else
                {
                    header.Append("To: ");
                }
                header.Append(__to);
                clrf = true;
            }

            if (__cc != null)
            {
                if (clrf)
                {
                    header.Append("\r\nCc: ");
                }
                else
                {
                    header.Append("Cc: ");
                }
                header.Append(__cc.ToString());
                clrf = true;
            }
            if (__isAppend && __bcc != null)
            {
                if (clrf)
                {
                    header.Append("\r\nBcc: ");
                }
                else
                {
                    header.Append("Bcc: ");
                }
                header.Append(__bcc.ToString());
                clrf = true;
            }
            if (__messageid != null)
            {
                if (clrf)
                {
                    header.Append("\r\nMessage-Id: ");
                }
                else
                {
                    header.Append("Message-Id: ");
                }
                header.Append(__messageid.ToString());
                clrf = true;
            }
            if (__mime != null)
            {
                if (clrf)
                {
                    header.Append("\r\nMIME-Version: ");
                }
                else
                {
                    header.Append("MIME-Version: ");
                }
                header.Append(__mime.ToString());
                clrf = true;
            }
            if (__contentype != null)
            {
                if (clrf)
                {
                    header.Append("\r\nContent-Type: ");
                }
                else
                {
                    header.Append("Content-Type: ");
                }
                header.Append(__contentype.ToString());
                clrf = true;
            }
            if (__headerFields.Length > 0)
            {
                header.Append("\r\n").Append(__headerFields.ToString());
            }
            header.Append("\r\n");
            header.Append("\r\n");

            return header.ToString();
        }

        public void clearHeaders()
        {
            if (__headerFields.Length > 0)
            {
                __headerFields.Remove(0, __headerFields.Length);
            }
        }

        public String headerMultiparttoString()
        {
            StringBuilder header = new StringBuilder();

            if (__headerFields.Length > 0)
            {
                header.Append(__headerFields.ToString());
            }
            header.Append("\r\n");
            header.Append("\r\n");

            return header.ToString();
        }
    }
}
