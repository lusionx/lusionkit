using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Facilities
{
    public struct QueryStringCons
    {
        public const string Action = "action";
        public const string Actor = "Actor";
        public const string ActorName = "ACTOR_NAME";
        public const string Auditing = "au";
        public const string AutoExit = "autoExit";
        public const string BloggerId = "bloggerId";
        public const string BlogId = "blogId";
        public const string CategoryID = "categoryid";
        public const string CategoryName = "categoryName";
        public const string Certification = "ctft";
        public const string CinemaId = "cinemaId";
        public const string Color = "color";
        public const string CompanyId = "cpy";
        public const string Constellation = "ct";
        public const string Date = "date";
        public const string Day = "day";
        public const string Days = "d";
        public const string Deleted = "del";
        public const string Director = "Director";
        public const string DirectorName = "DIRECTOR_NAME";
        public const string DVDId = "dvdId";
        public const string Email = "email";
        public const string EmailArr = "emails";
        public const string Forget = "forget";
        public const string FristLetter = "fl";
        public const string From = "from";
        public const string Genre = "gn";
        public const string GenreName = "Genre_name";
        public const string GoTo = "GoTo";
        public const string Group_MyGroupListType = "MyGroupListType";
        public const string Guid = "guid";
        public const string Id = "id";
        public const string ID_List = "ID_List";
        public const string ILike = "ilike";
        public const string ImageId = "ImageId";
        public const string ImageType = "imgtp";
        public const string ImageUrl = "imageUrl";
        public const string inviteid = "inviteid";
        public const string Inviter = "inviter";
        public const string KeyWord = "v";
        public const string Language = "lg";
        public const string lastfetch = "lastfetch";
        public const string Location = "lt";
        public const string LocationId = "LocationId";
        public const string LoginEmail = "LoginEmail";
        public const string Month = "month";
        public const string MOVIE_ID = "MOVIE_ID";
        public const string MovieEditPageType = "MovieEditPageType";
        public const string MovieId = "movieid";
        public const string MovieNum = "movienum";
        public const string MyFavoriteTheater = "myFavoriteTheater";
        public const string name = "name";
        public const string NID = "nid";
        public const string ObjectId = "objId";
        public const string ObjectType = "objtypeId";
        public const string OrderBizlogicNo = "OrderBizlogicNo";
        public const string PageID = "pid";
        public const string PageIndex = "start";
        public const string PageName = "pageName";
        public const string PageSubAreaID = "pagesubareaID";
        public const string ParentID = "prid";
        public const string ParentPageIDInTreeLevel = "ParentPageIDInTreeLevel";
        public const string Pass = "password";
        public const string PATH = "PATH";
        public const string PersonId = "personid";
        public const string PhotoID = "photoID";
        public const string Plot = "Plot";
        public const string preview = "preview";
        public const string PropertyTypeID = "ptid";
        public const string RecommendName = "recommendName";
        public const string RedirectUrl = "redirectUrl";
        public const string SearchAll = "all";
        public const string SearchCompany = "company";
        public const string SearchMovie = "movie";
        public const string SearchPerson = "person";
        public const string SearchType = "shtp";
        public const string Sex = "sex";
        public const string ShowtimeId = "showtimeid";
        public const string Sort = "sort";
        public const string Sound = "sound";
        public const string String = "s";
        public const string TagName = "tagname";
        public const string Text = "text";
        public const string Time = "time";
        public const string Timeslice = "timeslice";
        public const string Type = "type";
        public const string Url = "url";
        public const string UserId = "userId";
        public const string UserName = "UserName";
        public const string ValidateCode = "ValidateCode";
        public const string Year = "r";
        public const string YearColumn = "Year";
    }

    public class DateProperty
    {
        //Äê
        private int _Year = 0;
        public int Year
        {
            get { return _Year; }
            set { _Year = value; }
        }

        //ÔÂ
        private int _Month = 0;
        public int Month
        {
            get { return _Month; }
            set { _Month = value; }
        }

        //ÈÕ
        private int _Day = 0;
        public int Day
        {
            get { return _Day; }
            set { _Day = value; }
        }
    }
}
