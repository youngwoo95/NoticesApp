-- [1] Table: Notice(공지사항) 테이블
CREATE TABLE [dbo].[Notices]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1), -- Serial Number
	[ParentId] int null,						 -- ParentId, AppId, SiteId, ...

	[Name] Nvarchar(255) Not null,				 -- 작성자
	[Title] Nvarchar(255) Null,				     -- 제목
	[Content] Nvarchar(Max) null,				 -- 내용
	[IsPinned] Bit Null Default(0),				 -- 공지글로 올리기
	[CreateBy] nvarchar(255) null,				 -- 등록자(Creator)
	[Created] DateTime default(GetDate()) null,  -- 생성일(PostDate)
	[ModifiedBy] nvarchar(255) null,			 -- 수정자(LastModifiedBy)
	[Modified] DateTime null,					 -- 수정일(LastModified)
)
GO
