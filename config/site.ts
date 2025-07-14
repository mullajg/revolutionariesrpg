export type SiteConfig = typeof siteConfig;

export const siteConfig = {
  name: "Revolutionaries",
  description: "A website for Revolutionaries RPG. Gameplay by Hunter Allan. Website by James Mullan.",
  navItems: [
    {
        label: "Home",
        href: "/",
    },
    {
        label: "My Characters",
        href: "/mycharacters",
    },
    {
        label: "Create New Character",
        href: "/createnewcharacter",
    },
    {
        label: "Compendium",
        href: "/compendium"
    }
  ],
  navMenuItems: [
    {
        label: "Home",
        href: "/",
    },
    {
        label: "My Characters",
        href: "/mycharacters",
    },
    {
        label: "Create New Character",
        href: "/createnewcharacter",
    },
    {
        label: "Compendium",
        href: "/compendium"
    },
    {
      label: "Logout",
      href: "/logout",
    },
  ],
  links: {
    roll20: "https://app.roll20.net/campaigns/details/18382217/revolutionaries-shifting-sands",
    twitter: "https://twitter.com/hero_ui",
    docs: "https://heroui.com",
    discord: "https://discord.gg/9b6yyZKmH4",
    sponsor: "https://patreon.com/jrgarciadev",
    github: "https://github.com/mullajg/revolutionariesrpg",
    baseApiUrl: "https://revolutionariesrpg.com/api",

  },
};
